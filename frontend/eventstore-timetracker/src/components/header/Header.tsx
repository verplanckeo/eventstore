import { Schedule } from "@mui/icons-material";
import { AppBar, Toolbar, Typography } from "@mui/material";
import type React from "react";

export const Header: React.FC = () => {
	return (
		<AppBar position="static" elevation={0}>
			<Toolbar>
				<Schedule sx={{ mr: 2 }} />
				<Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
					Timetracker - ITIGAI
				</Typography>
			</Toolbar>
		</AppBar>
	);
};
