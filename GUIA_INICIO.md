# Guía de Inicio - Seguridad Ciudadana UTA

Para que el sistema funcione correctamente, debes iniciar los componentes en el siguiente orden:

## 1. Configuración de Red (IMPORTANTE)
Como la App se conecta desde un celular físico a tu PC, debes asegurarte de que:
1.  Tu PC y tu celular estén en la **misma red Wi-Fi**.
2.  Verificar tu IP local (ej: `192.168.0.5`) y asegurarte de que coincida en:
    *   `Frontend/context/AuthContext.js` -> `API_URL`
    *   `Frontend/screens/GuardScreen.js` -> `signalR.withUrl`

---

## 2. Iniciar el Backend
1.  Abre una terminal en `Backend/SeguridadUta.Api`.
2.  Ejecuta:
    ```powershell
    dotnet run --urls "http://0.0.0.0:5000"
    ```
    *(Nota: Esto restaura las librerías de C# automáticamente).*

## 3. Iniciar el Panel Web (Admin)
1.  Abre una terminal en `AdminWeb`.
2.  **Primero instala las librerías (solo la primera vez):**
    ```powershell
    npm install
    ```
3.  Luego inicia:
    ```powershell
    npm run dev
    ```

## 4. Iniciar la App Móvil
1.  Abre una terminal en `Frontend`.
2.  **Primero instala las librerías (solo la primera vez):**
    ```powershell
    npm install
    ```
3.  Luego inicia (asegúrate de poner tu IP):
    ```powershell
    set REACT_NATIVE_PACKAGER_HOSTNAME=192.168.x.x
    npx expo start --lan
    ```
3.  Escanea el código QR con la app **Expo Go** en tu celular.
4.  **Logins de prueba**:
    *   **Estudiante**: `estudiante1@uta.edu.ec` / `123456`
    *   **Guardia**: `guardia1@uta.edu.ec` / `123456`

---

## 🔑 Credenciales DITIC (Simuladas)
*   **Admin**: `admin@uta.edu.ec` / `admin123`
*   **Estudiantes**: `estudiante1` hasta `estudiante5@uta.edu.ec` / `123456`
*   **Guardias**: `guardia1` hasta `guardia5@uta.edu.ec` / `123456`
