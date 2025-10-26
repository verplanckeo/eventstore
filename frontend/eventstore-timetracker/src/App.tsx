import './App.css'
import { Navigate, Route, Routes } from "react-router-dom";
import { NavigationBar } from './components/navbar/NavigationBar'
import { Header } from './components/header/Header'
import NotificationContainer from './components/notification/NotificationContainer'
import { RegisterUser } from './pages/users/register';

function App() {

  return (
    <>
			<Header />
			<Routes>
				<Route path="/" element={<Navigate to="/user-new" replace />} />
				<Route path="/user-new" element={<RegisterUser />} />
				{/* <Route path="/user-overview" element={<Overview />} /> */}

			</Routes>
			<NotificationContainer />
			<NavigationBar />
    </>
  )
}

export default App
