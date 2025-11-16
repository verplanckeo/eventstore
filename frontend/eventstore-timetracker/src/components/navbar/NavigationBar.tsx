// frontend/eventstore-timetracker/src/components/navbar/NavigationBar.tsx
import { Assessment, Dashboard, Login as LoginIcon, PersonAdd, AccessTime, Work } from "@mui/icons-material";
import { BottomNavigation, BottomNavigationAction } from "@mui/material";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import { useAuth } from "../../hooks/use-auth";

interface NavItem {
	label: string;
	path: string;
	icon: React.ReactNode;
}

export const NavigationBar: React.FC = () => {
	const [currentView, setCurrentView] = useState<number>(0);
	const navigate = useNavigate();
	const location = useLocation();
	const { isAuthenticated } = useAuth();

	// Define navigation items based on authentication status
	const guestNavItems: NavItem[] = [
		{ label: "Login", path: "/login", icon: <LoginIcon /> },
		{ label: "Register", path: "/register", icon: <PersonAdd /> },
	];

	const authenticatedNavItems: NavItem[] = [
		{ label: "Dashboard", path: "/dashboard", icon: <Dashboard /> },
		{ label: "Time Entries", path: "/time-entries", icon: <AccessTime /> },
		{ label: "Projects", path: "/projects", icon: <Work /> },
		{ label: "Analytics", path: "/analytics", icon: <Assessment /> },
	];

	// Select the appropriate nav items based on authentication status
	const navItems = isAuthenticated ? authenticatedNavItems : guestNavItems;

	// Update currentView based on the current pathname
	useEffect(() => {
		const currentIndex = navItems.findIndex(
			(item) => item.path === location.pathname
		);
		if (currentIndex !== -1) {
			setCurrentView(currentIndex);
		} else {
			// If the current path doesn't match any nav item, reset to first item
			setCurrentView(0);
		}
	}, [location.pathname, navItems]);

	const handleNavItemClick = (
		path: string,
		selectedViewIndex: number
	): void => {
		setCurrentView(selectedViewIndex);
		navigate(path);
	};

	return (
		<BottomNavigation
			value={currentView}
			sx={{
				position: "fixed",
				bottom: 0,
				left: 0,
				right: 0,
				elevation: 3,
				borderTop: 1,
				borderColor: "divider",
			}}
		>
			{navItems.map((item, index) => (
				<BottomNavigationAction
					key={item.path}
					label={item.label}
					icon={item.icon}
					onClick={() => handleNavItemClick(item.path, index)}
				/>
			))}
		</BottomNavigation>
	);
};