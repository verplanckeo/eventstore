import './App.css'
import { Navigate, Route, Routes } from "react-router-dom";
import { NavigationBar } from './components/navbar/NavigationBar'
import { Header } from './components/header/Header'
import NotificationContainer from './components/notification/NotificationContainer'
import { RegisterUser } from './pages/users/register';
import { Login } from './pages/auth';
import { Dashboard } from './pages/dashboard';
import { GuestGuard } from './auth/guard/guest-guard';
import { AuthGuard } from './auth/guard/auth-guard';
import { Projects } from './pages/project';
import { TimeEntries } from './pages/timeentry';
import { Analytics } from './pages/analytics';

function App() {

  return (
    <>
		<Header />
		<Routes>
			<Route path="/" element={<Navigate to="/login" replace />} />
				{/* Public routes with GuestGuard */}
				<Route
					path="/login"
					element={
						<GuestGuard>
							<Login />
						</GuestGuard>
					}
				/>
				<Route
					path="/register"
					element={
						<GuestGuard>
							<RegisterUser />
						</GuestGuard>
					}
				/>

				{/* Protected routes with AuthGuard */}
				<Route
					path="/dashboard"
					element={
						<AuthGuard>
							<Dashboard />
						</AuthGuard>
					}
					/>

				{/* Protected routes with AuthGuard */}
				<Route
					path="/projects"
					element={
						<AuthGuard>
							<Projects />
						</AuthGuard>
					}
					/>

				{/* Time Entries */}
				<Route
					path="/time-entries"
					element={
						<AuthGuard>
							<TimeEntries />
						</AuthGuard>
					}
					/>

			{/* Analytics */}
			<Route
				path="/analytics"
				element={
					<AuthGuard>
						<Analytics />
					</AuthGuard>
				}
				/>
					
			{/* <Route path="/user-overview" element={<Overview />} /> */}

		</Routes>
		<NotificationContainer />
		<NavigationBar />
    </>
  )
}

export default App
