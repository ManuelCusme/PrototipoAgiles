# Seguridad Ciudadana UTA - Prototipo Ágiles

## 🎯 Objetivo del Proyecto
Este sistema es una solución integral de seguridad diseñada para el campus de la Universidad Técnica de Ambato (UTA). Su propósito es reducir el tiempo de respuesta ante incidentes (robos, accidentes, acoso) mediante una red de comunicación en tiempo real entre estudiantes, guardias y personal administrativo.

## 🏗️ Arquitectura del Sistema
El sistema se divide en tres componentes principales que trabajan sincronizados:

### 1. Backend (.NET 10 API + SignalR)
*   **Cerebro del Sistema**: Gestiona la base de datos de usuarios (Simulación DITIC).
*   **Comunicación en Tiempo Real**: Utiliza **SignalR** para emitir alertas instantáneas. Cuando un estudiante presiona el botón, la señal llega a todos los guardias y administradores en menos de 1 segundo.
*   **Geocercas**: Determina automáticamente en qué zona de la universidad se encuentra el incidente.

### 2. App Móvil (Expo / React Native) - Multi-Rol
*   **Módulo Estudiante**: 
    *   Interfaz simplificada con selector de motivo.
    *   **Botón de Pánico Táctico**: Requiere 3 segundos de presión para evitar falsas alarmas.
    *   Envía coordenadas GPS exactas.
*   **Módulo Guardia**:
    *   Mapa táctico con 4 zonas de seguridad (Z1-Z4).
    *   Notificaciones con vibración y sonido.
    *   Visualización de quién reporta y por qué.

### 3. Panel Administrativo Web (Vite + React + Leaflet)
*   **Sala de Guerra (War Room)**: Un mapa interactivo que muestra todas las zonas del campus.
*   **Gestión de Incidentes**: Permite visualizar, rastrear y descartar alertas activas.
*   **Modo Táctico**: Diseño oscuro optimizado para centros de control.

## 🛡️ Características Tácticas (Basadas en Requerimientos)
*   **Zonificación**: El campus está dividido en 4 cuadrantes tácticos para despliegue rápido de guardias.
*   **Identificación DITIC**: Solo usuarios institucionales pueden acceder, identificando su facultad de origen.
*   **Validación de Motivos**: Los incidentes se categorizan (Robo, Arma Blanca, etc.) para que el guardia sepa a qué se enfrenta antes de llegar.
