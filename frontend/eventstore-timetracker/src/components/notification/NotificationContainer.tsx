import React from "react";
import { Snackbar, Alert, Box } from "@mui/material";
import { useNotification } from "./hooks/use-notification";

const NotificationContainer: React.FC = () => {
	const { notifications, hideNotification } = useNotification();

	return (
		<Box>
			{notifications.map((notification) => (
				<Snackbar
					key={notification.id}
					open={true}
					autoHideDuration={notification.autoHideDuration}
					onClose={() => hideNotification(notification.id)}
					anchorOrigin={{
						vertical: "top",
						horizontal: "right",
					}}
					sx={{
						// Stack multiple notifications
						position: "relative",
						mb: notifications.length > 1 ? 1 : 0,
					}}
				>
					<Alert
						severity={notification.severity}
						onClose={() => hideNotification(notification.id)}
						variant="filled"
						elevation={6}
						sx={{
							minWidth: "300px",
							maxWidth: "500px",
						}}
					>
						{notification.message}
					</Alert>
				</Snackbar>
			))}
		</Box>
	);
};

export default NotificationContainer;
