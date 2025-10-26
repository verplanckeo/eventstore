import { Add, Assessment } from "@mui/icons-material";
import { BottomNavigation, BottomNavigationAction } from "@mui/material";
import { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";

interface NavItem {
	label: string;
	path: string;
	icon: React.ReactNode;
}

export const NavigationBar: React.FC = () => {
	const [currentView, setCurrentView] = useState<number>(0); // 0 = add, 1 = overview
	const navigate = useNavigate();
	const location = useLocation();

	const navItems: NavItem[] = [
		{ label: "Add Entry", path: "/add", icon: <Add /> },
		{ label: "Overview", path: "/overview", icon: <Assessment /> },
	];

	// Update currentView based on the current pathname
	useEffect(() => {
		const currentIndex = navItems.findIndex(
			(item) => item.path === location.pathname
		);
		if (currentIndex !== -1) {
			setCurrentView(currentIndex);
		}
	}, [location.pathname]);

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
