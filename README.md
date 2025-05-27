# 🎮 Workshop Peripherals – Bac2 Q2

🇫🇷 [Version française](#français-)  
🇬🇧 [English version](#english-)

---

## Français 🇫🇷

> Prototype de jeu Unity utilisant un contrôleur physique personnalisé basé sur Arduino, développé dans le cadre du workshop "Périphériques" du Bac2 Q2 à la Haute École Albert Jacquard.

---

### 🧩 Fonctionnalités

- Contrôle du jeu via un périphérique physique personnalisé
- Communication série entre Unity et Arduino
- Intégration de capteurs et boutons pour interagir avec le jeu
- Feedback visuel en temps réel

---

### 🛠️ Technologies

- **Moteur** : Unity
- **Langage** : C# et C
- **Microcontrôleur** : Arduino
- **Communication** : Port série
- **Versioning** : Git & GitHub

---

### 📁 Structure du projet

```plaintext
📁 Assets/
├── Scripts/           # Scripts C# pour la communication et le gameplay
├── Scenes/            # Scènes Unity
├── Prefabs/           # Objets préfabriqués
├── UI/                # Éléments de l'interface utilisateur
📁 Arduino/
├── Controller.ino     # Code Arduino pour le contrôleur personnalisé
```

---

### 🚀 Lancer le projet

1. **Cloner le dépôt** :
   ```bash
   git clone https://github.com/moust1que/Workshop-Peripherals-Bac2-Q2.git
   ```
2. **Configurer Unity** :
   - Version recommandée : Unity 6000.0.38f1 LTS ou supérieure
   - Ouvrir le dossier via Unity Hub
3. **Vérifier que le niveau de compatibilité API est défini sur .NET 4.x** :
   - Éditeur Unity → Edit > Project Settings > Player > Other Settings > Api Compatibility Level → .NET 4.x
4. **Connecter l'Arduino** :
   - Charger le fichier Controller.ino sur la carte Arduino via l'IDE Arduino
   - Connecter la carte Arduino au PC via USB
5. **Configurer le port série dans Unity** :
   - Dans le script C#, définir le nom du port série correspondant (ex. COM3 sur Windows ou /dev/ttyUSB0 sur Linux)
6. **Lancer la scène principale** :
   - Ouvrir la scène SampleScene.unity dans Assets/Scenes/
   - Appuyer sur ▶️ pour lancer le jeu

---

### 👨‍💻 Auteurs

- [@moust1que](https://github.com/moust1que) Prog Arduino
- [@Maxime-Code-Git](https://github.com/Maxime-Code-Git) Prog Unity
- [@AlexandreMeulders](https://github.com/AlexandreMeulders) Game Designer
- Julien Game Designer
- Angelika Game Artist
- Antoine Game Artist

---

### 🏫 Crédits

> Projet réalisé dans le cadre du workshop Périphériques à la HEAJ - Haute École Albert Jacquard

---

### 📄 Licence

> Ce projet est un travail académique. Aucune licence open source officielle ne s'applique.

---

## English 🇬🇧

> Unity game prototype using a custom physical controller based on Arduino, developed as part of the "Peripherals" workshop in Bac2 Q2 at Haute École Albert Jacquard college.

---

### 🧩 Features

- Game control via a custom physical device
- Serial communication between Unity and Arduino
- Integration of sensors and buttons to interact with the game
- Real-time visual feedback

---

### 🛠️ Technologies

- **Engine**: Unity
- **Language**: C#
- **Microcontroller**: Arduino
- **Communication**: Serial port
- **Versioning**: Git & GitHub

---

### 📁 Project Structure

```plaintext
📁 Assets/
├── Scripts/           # C# scripts for communication and gameplay
├── Scenes/            # Unity scenes
├── Prefabs/           # Prefabricated objects
├── UI/                # User interface elements
📁 Arduino/
├── Controller.ino     # Arduino code for the custom controller
```

---

### 🚀 Running the Project

1. **Clone the repository**:
   ```bash
   git clone https://github.com/moust1que/Workshop-Peripherals-Bac2-Q2.git
   ```
2. **Set up Unity**:
   - Recommended version: Unity 6000.0.38f1 LTS or later
   - Open the folder via Unity Hub
3. **Ensure the API compatibility level is set to .NET 4.x**:
   - Unity Editor → Edit > Project Settings > Player > Other Settings > Api Compatibility Level → .NET 4.x
4. **Connect the Arduino**:
   - Upload the Controller.ino file to the Arduino board using the Arduino IDE
   - Connect the Arduino board to the PC via USB
5. **Configure the serial port in Unity**:
   - In the C# script, set the serial port name accordingly (e.g., COM3 on Windows or /dev/ttyUSB0 on Linux)
6. **Launch the main scene**:
   - Open the SampleScene.unity scene in Assets/Scenes/
   - Press ▶️ to start the game

---

### 👨‍💻 Authors

- [@moust1que](https://github.com/moust1que) Prog Arduino
- [@Maxime-Code-Git](https://github.com/Maxime-Code-Git) Prog Unity
- [@AlexandreMeulders](https://github.com/AlexandreMeulders) Game Designer
- Julien Game Designer
- Angelika Game Artist
- Antoine Game Artist

---

### 🏫 Credits

> Project created for the Peripherals workshop at HEAJ - Haute École Albert Jacquard

---

### 📄 License

> This project was created for academic purposes. No official open source license is applied.
