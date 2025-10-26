// contexts/NotificationContext.tsx
import React, { useState, type ReactNode } from "react";
import { NotificationContext } from "./notification.context";

export interface Notification {
	id: string;
	message: string;
	severity: "success" | "error" | "warning" | "info";
	autoHideDuration?: number;
}

export interface NotificationContextType {
	notifications: Notification[];
	showNotification: (notification: Omit<Notification, "id">) => void;
	hideNotification: (id: string) => void;
	showSuccess: (message: string, autoHideDuration?: number) => void;
	showError: (message: string, autoHideDuration?: number) => void;
	showWarning: (message: string, autoHideDuration?: number) => void;
	showInfo: (message: string, autoHideDuration?: number) => void;
}

interface NotificationProviderProps {
	children: ReactNode;
}

export const NotificationProvider: React.FC<NotificationProviderProps> = ({
	children,
}) => {
	const [notifications, setNotifications] = useState<Notification[]>([]);

	const generateId = (): string => {
		return Math.random().toString(36).substr(2, 9);
	};

	const showNotification = (notification: Omit<Notification, "id">): void => {
		const id = generateId();
		const newNotification: Notification = {
			...notification,
			id,
			autoHideDuration: notification.autoHideDuration ?? 6000,
		};

		setNotifications((prev) => [...prev, newNotification]);

		// Auto-hide after specified duration
		if (newNotification.autoHideDuration! > 0) {
			setTimeout(() => {
				hideNotification(id);
			}, newNotification.autoHideDuration);
		}
	};

	const hideNotification = (id: string): void => {
		setNotifications((prev) =>
			prev.filter((notification) => notification.id !== id)
		);
	};

	// Convenience methods for different severity types
	const showSuccess = (message: string, autoHideDuration = 4000): void => {
		showNotification({ message, severity: "success", autoHideDuration });
	};

	const showError = (message: string, autoHideDuration = 8000): void => {
		showNotification({ message, severity: "error", autoHideDuration });
	};

	const showWarning = (message: string, autoHideDuration = 6000): void => {
		showNotification({ message, severity: "warning", autoHideDuration });
	};

	const showInfo = (message: string, autoHideDuration = 5000): void => {
		showNotification({ message, severity: "info", autoHideDuration });
	};

	const value: NotificationContextType = {
		notifications,
		showNotification,
		hideNotification,
		showSuccess,
		showError,
		showWarning,
		showInfo,
	};

	return (
		<NotificationContext.Provider value={value}>
			{children}
		</NotificationContext.Provider>
	);
};
