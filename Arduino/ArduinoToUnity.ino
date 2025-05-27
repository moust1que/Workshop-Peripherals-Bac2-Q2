#include <Wire.h>
#include <Stepper.h>
#include <GY521.h>

#define STEPS 32

/*-----------------Buttons-----------------*/
const int button1Pin = 0x16; // 22
const int button2Pin = 0x18; // 24
const int button3Pin = 0x1a; // 26

bool button1State = 0;
bool button2State = 0;
bool button3State = 0;

/*----------------Gyroscope----------------*/
GY521 sensor(0x68);

float pitch, roll, yaw;

/*----------------Joystick-----------------*/
const int joystickXPin = A0;
const int joystickYPin = A1;
const int joystickButton = 0x05; // 5

int joystickXValue = 512;
int joystickYValue = 512;
bool joystickClicked = 0;

/*------------RotaryAndStepper-------------*/
const int rotaryDTPin = 0x03; // 3
const int rotarySWPin = 0x04; // 4
const int rotaryTarget = 40; // 100 by default

int counter = 0;
int rotaryState;
int rotaryLastState;

const int stepsMultiplier = 100;
const int stepperSpeed = 1000;

int prevPosition;
int stepsToTake;
int totalSteps = 0;

Stepper small_stepper(STEPS, 8, 10, 9, 11);

bool isLoaded = false;
bool isAiming = false;
bool shootTriggered = false;
bool releasing = false;

/*----------------Functions----------------*/
void setup() {
  Serial.begin(9600);
  delay(1000);
  
  pinMode(button1Pin, INPUT_PULLUP);
  pinMode(button2Pin, INPUT_PULLUP);
  pinMode(button3Pin, INPUT_PULLUP);
  
  Wire.begin();
  Wire.setClock(400000);
  
  if(!sensor.begin())
    while(1);

  Serial.println("GY521 détecté !");
  
  sensor.setAccelSensitivity(0);
  sensor.setGyroSensitivity(0);
  
  Serial.println("Calibration du GY521...");
  if(sensor.calibrate(100, 0, 0, false))
    Serial.println("Calibration terminée.");
  else
    Serial.println("Erreur de calibration !");

  sensor.setNormalize(true);
  
  pinMode(joystickButton, INPUT_PULLUP);
  
  pinMode(rotaryDTPin, INPUT_PULLUP);
  pinMode(rotarySWPin, INPUT_PULLUP);
  rotaryLastState = digitalRead(rotaryDTPin);
  
  Serial.println("Setup terminé.");
}

void loop() {
  recordButtons();

  sensor.read();
  
  pitch = sensor.getPitch();
  roll  = sensor.getRoll();
  yaw   = sensor.getYaw();

  recordJoystick();

  if(!isLoaded) {
    handleRotary();
    handleStepper();
  }else if(shootTriggered) {
    isAiming = false;
    releaseStepper();
  }

  sendAllData();

  delay(1000 / 60);
}

void recordButtons() {
  static bool lastButton1State = HIGH;
  static bool lastButton2State = HIGH;
  static bool lastButton3State = HIGH;

  button1State = digitalRead(button1Pin);
  button2State = digitalRead(button2Pin);
  button3State = digitalRead(button3Pin);

  if (button1State == LOW && lastButton1State == HIGH) {
    isAiming = !isAiming;
    delay(200);
  }
  lastButton1State = button1State;

  if (button3State == LOW && lastButton3State == HIGH && isLoaded) {
    shootTriggered = true;
    delay(200);
  }
  lastButton3State = button3State;
}

void recordJoystick() {
  joystickClicked = !digitalRead(joystickButton);

  joystickXValue = analogRead(joystickXPin);
  joystickYValue = analogRead(joystickYPin);
}

bool isRotating = false;
void handleRotary() {
  rotaryState = digitalRead(rotaryDTPin);

  if(rotaryState != rotaryLastState) {
    counter++;
    isRotating = true;
    rotaryLastState = rotaryState;
  }
  else
    isRotating = false;

  if(counter >= rotaryTarget) {
    isLoaded = true;
    counter = rotaryTarget;
  }

}

void handleStepper() {
  small_stepper.setSpeed(stepperSpeed);

  // if(totalSteps < counter * stepsMultiplier) {
  if(counter > 0 && isRotating) {
    
  // Serial.print(counter);
    totalSteps += stepsMultiplier;
    small_stepper.step(stepsMultiplier);
  }

  digitalWrite(8, LOW);
  digitalWrite(9, LOW);
  digitalWrite(10, LOW);
  digitalWrite(11, LOW);
}

void releaseStepper() {
  if (totalSteps > 0) {
    small_stepper.step(-stepsMultiplier);
    totalSteps -= stepsMultiplier;
    counter--;
  }
  
  if (totalSteps <= 0) {
    totalSteps = 0;
    isLoaded = false;
    shootTriggered = false;
    counter = 0;
  }
}

void sendAllData() {
  button1State == 0 ? Serial.print("1 ") : Serial.print("0 ");
  button2State == 0 ? Serial.print("1 ") : Serial.print("0 ");
  button3State == 0 ? Serial.print("1 ") : Serial.print("0 ");

  Serial.print(pitch);
  Serial.print(" ");
  Serial.print(roll);
  Serial.print(" ");
  Serial.print(yaw);
  Serial.print(" ");

  Serial.print(joystickClicked);
  Serial.print(" ");
  Serial.print(joystickXValue);
  Serial.print(" ");
  Serial.print(joystickYValue);
  Serial.print(" ");

  Serial.print(isLoaded);
  Serial.print(" ");
  Serial.print(isAiming);
  Serial.print(" ");
  Serial.print(shootTriggered);

  Serial.println();
}