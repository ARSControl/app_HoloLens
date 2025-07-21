# app_HoloLens

This repository contains an application developed with Unity and thought to be deployed to Microsoft HoloLens 2. It consists of multiple games that aim to stimulate cognitive functioning and visuo-spatial skills for elderly individuals. 

<sub>This work was supported by the project '"Cognifit Harmony", funded by Horizon Europe program as Financial Support for Third Parties (FSTP) of the project "UTTER" (GA 101070631), and by the "Lively Ageing" project, funded by the Italian Ministry of Health.</sub>

## Prerequisites
- Download **Unity 2020.3.46f1**
- Download **Visual Studio 2022**
  * Sviluppo ASP.Net e Web
  * Sviluppo di Azure
  * Sviluppo per desktop .NET
  * Sviluppo di applicazioni desktop con C++
  * Sviluppo di app per la piattaforma UWP (Connettività USB, Strumenti piattaforma UWP v143, Windows 11 SDK)
  * Sviluppo giochi con Unity

## Unity Setup
1. Open Unity project
2. Go to **File** --> **Build Settings**
3. Select **UWP** as the platform
4. Set Target Device to **HoloLens**
5. Set Architecture to **ARM64**
6. Set Build and Run on **Remote Device** for Wi-Fi deployment or **Local MAchine** for USB deployment
7. Click **Switch Platform**

## MRTK Setup
- Download the [Mixed Reality Feature Tool.](https://www.microsoft.com/en-us/download/details.aspx?id=102778)
- Select the features:
  * Mixed Reality Toolkit: Mixed Reality Toolkit Examples, Mixed Reality Toolkit Extensions, Mixed Reality Toolkit Foundation, Mixed Reality Toolkit Standard Asset
  * Platform Support: Mixed Reality OpenXR Plugin
  * World Locking Tools: WLT Core, WLT Samples

To set up the MRTK toolkit, follow these guides:
1. [Welcome to the Mixed Reality Feature Tool](https://learn.microsoft.com/en-us/windows/mixed-reality/develop/unity/welcome-to-mr-feature-tool)
2. [Introduction to the Mixed Reality Toolkit-Set Up Your Project and Use Hand Interaction](https://learn.microsoft.com/en-us/training/modules/learn-mrtk-tutorials/)

## Build and Deploy to the HoloLens
Follow [this guide](https://learn.microsoft.com/en-us/windows/mixed-reality/develop/unity/build-and-deploy-to-hololens#build-the-unity-project) to build and deploy the project to the HoloLens.
