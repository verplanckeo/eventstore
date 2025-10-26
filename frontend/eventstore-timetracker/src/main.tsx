
import App from "./App";
import { ThemeProvider } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { StrictMode } from "react";
import { createRoot } from 'react-dom/client'
import { BrowserRouter } from "react-router-dom";
import './index.css'
import { NotificationProvider } from "./components/notification/notification.provider.tsx";
import theme from "./theme.ts";
import { AuthProvider } from "./auth/auth.provider.tsx";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
		<AuthProvider>
			<BrowserRouter>
				<NotificationProvider>
					<ThemeProvider theme={theme}>
						<CssBaseline />
						<LocalizationProvider dateAdapter={AdapterDayjs}>
							<App />
						</LocalizationProvider>
					</ThemeProvider>
				</NotificationProvider>
			</BrowserRouter>
		</AuthProvider>
	</StrictMode>
)
