# 📌 Project Summary

This project focuses on the acquisition and processing of signals from two inertial measurement units (IMUs) to study the behavior of the knee and hip during walking. The primary objective was to model and mitigate measurement noise to enable accurate tracking of joint angles in gait analysis.

Key components include:

- Theoretical study of human gait cycle, IMU technologies (accelerometers, gyroscopes), and error modeling.

- Design and implementation of a mechanical platform (robotic arm with 2 degrees of freedom) simulating lower limb motion.

- Development of an electronic system (MPU-6050 sensors, ATmega2560 microcontroller, Bluetooth communication) for signal acquisition and wireless data transmission.

- Creation of a software interface to visualize data online and facilitate real-time monitoring.

- Statistical analysis and noise modeling using Allan variance, enabling classification of sensor errors and validation of sensor reliability for angle estimation.

- Final gait tracking experiments confirming the system’s ability to monitor knee and hip motion accurately during walking.

---

# Knee and Hip Behavior Analysis with Dual IMU System

**Author:** Luna Salameh  
**Date:** June - September 2022
---

## 📌 Project Overview

This project implements a system for acquiring, processing, and visualizing motion signals from two **MPU-6050 IMUs** placed at the knee and hip joints. The system aims to:
- Track joint angles during walking.
- Filter sensor noise for accurate motion analysis.
- Provide real-time visualization of both raw and processed signals.
- Collect datasets for gait studies on volunteers.

---

## ⚙️ Main Components

### 🔹 Hardware
- Dual MPU-6050 IMUs
- ATmega2560 microcontroller (Arduino Mega)
- Bluetooth module for wireless data transmission

---

### 🔹 Software
- **Arduino code**: Handles IMU data acquisition and Bluetooth communication.
- **MATLAB scripts**: Process and filter accelerometer and gyroscope signals (e.g., Kalman filter, Allan variance analysis).
- **C# display app**: Visualizes both raw and filtered IMU signals in real time.

---

## 🚀 How to Run

1️⃣ Upload the Arduino code to your ATmega2560 board.  
2️⃣ Start the C# display app to view real-time signals.  
3️⃣ Use MATLAB scripts to post-process saved data files.  
4️⃣ Review figures for results of filtering and tracking.

---

## 🗂 Dataset

Collected from volunteers walking over defined periods.  
See `final/data_set` for sample records.

---

## 📄 License

This project was developed for academic and research purposes at HIAST, Syria.  
Feel free to use and adapt with attribution.


