# Seguridad Ciudadana UTA

Sistema de seguridad comunitaria con backend .NET 8 y frontend móvil en React Native (Expo).

## 🚀 Requisitos
- SQL Server (LocalDB o instancia completa).
- .NET 8 SDK.
- Node.js (v18+).
- Expo Go en el móvil (para pruebas).

## 🛠️ Configuración

### 1. Base de Datos
Ejecute el script `Database.sql` en su instancia de SQL Server para crear las tablas y las geocercas iniciales.

### 2. Backend
1. Vaya a `Backend/SeguridadUta.Api`.
2. Actualice la cadena de conexión en `appsettings.json` si es necesario.
3. Ejecute: `dotnet run`.

### 3. Frontend
1. Vaya a `Frontend`.
2. Actualice la `API_URL` en `context/AuthContext.js` con su dirección IP local (ej: `192.168.1.10`).
3. Ejecute: `npm start`.

## 📱 Funcionalidades
- **Registro/Login**: Con validación de edad (> 13 años) y desglose de 4 nombres.
- **Mapa Interactivo**: Visualización de ubicación actual y geocercas configuradas.
- **Botón de Pánico**: Envío de alertas instantáneas con detección automática de zona (geocerca).
- **Tiempo Real**: Notificaciones vía SignalR para todos los usuarios conectados.

## 📍 Geocercas Sembradas
- Campus Huachi (-1.2692, -78.6242) - Radio 500m
- Campus Ingahurco (-1.2422, -78.6251) - Radio 300m
- Facultad Sistemas (-1.2655, -78.6210) - Radio 100m
