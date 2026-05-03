# Seguridad Ciudadana UTA - Sistema Táctico Real-Time 🚨🏛️

Prototipo de seguridad integral para la Universidad Técnica de Ambato (UTA). Este sistema permite la gestión inmediata de incidentes mediante una red sincronizada entre Estudiantes, Guardias y Administradores.

## 🚀 Tecnologías
- **Backend**: .NET 10 Web API + SignalR (Real-time).
- **Base de Datos**: SQL Server (Simulación DITIC).
- **App Móvil**: React Native + Expo + React Native Paper.
- **Panel Web**: React + Vite + Leaflet Maps.

## 🛡️ Funcionalidades Clave
- **Multi-Rol**: Interfaces diferenciadas para Estudiantes (Pánico), Guardias (Respuesta) y Admin (Control).
- **Botón de Pánico Inteligente**: Activación por presión prolongada (3 seg) para evitar falsas alarmas.
- **Zonificación Táctica**: Campus dividido en 4 cuadrantes (Z1-Z4) para despliegue rápido.
- **Categorización de Emergencias**: Selección de motivos (Robo, Accidente, Acoso, etc.).
- **Simulación DITIC**: Usuarios precargados mediante sistema de seeding automático.

## 🛠️ Configuración e Instalación

### 1. Backend
- Navegar a `Backend/SeguridadUta.Api`.
- El sistema utiliza SQL Server. Al ejecutar por primera vez, se creará la base de datos y se sembrarán los 11 usuarios institucionales automáticamente.
- Comando: `dotnet run --urls "http://0.0.0.0:5000"`

### 2. Panel Administrativo Web
- Navegar a `AdminWeb`.
- Instalar dependencias: `npm install`.
- Ejecutar: `npm run dev`.
- **Acceso Admin**: `admin@uta.edu.ec` / `admin123`.

### 3. App Móvil (Frontend)
- Navegar a `Frontend`.
- Instalar dependencias: `npm install`.
- **Configuración IP**: Actualizar la IP de tu PC en `context/AuthContext.js` y `screens/GuardScreen.js`.
- Ejecutar: `set REACT_NATIVE_PACKAGER_HOSTNAME=tu.ip.local` y luego `npx expo start --lan`.

## 📍 Geocercas y Zonas
El sistema integra cuadrantes tácticos sobre el Campus Huachi:
- **Zona 1**: Ingeniería.
- **Zona 2**: Administración.
- **Zona 3**: Deportes.
- **Zona 4**: Idiomas.

---
*Desarrollado para la materia de Metodologías Ágiles - Universidad Técnica de Ambato.*
