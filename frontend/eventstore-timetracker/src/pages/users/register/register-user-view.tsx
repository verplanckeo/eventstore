import { useState } from "react";
import { Container, Alert } from "@mui/material";
import type { RegisterUserResponse } from "../../../interfaces/registeruserresponse.interface";
import RegisterUser from "../../../components/users/register/RegisterUser";
import { useNotification } from "../../../components/notification/hooks/use-notification";

function RegisterUserView() {
	// keep a separate error state for mutations (saving/updating/deleting)
	const [mutationError, setMutationError] = useState<string | null>(null);
	const { showSuccess, showError } = useNotification();

	const registerUser = async (
		newUser: RegisterUserResponse
	): Promise<{ success: boolean; entry?: RegisterUserResponse; error?: string }> => {
		
		try {
			setMutationError(null);
			showSuccess("User registered successfully.");
			return { success: true, entry: newUser };
		} catch {
			const errorMessage = "Failed to save entry. Please try again.";
			setMutationError(errorMessage);
			showError(errorMessage);
			return { success: false, error: errorMessage };
		} finally {
		}
	};

	const handleCloseError = (): void => {
		// close either mutation or load errors
		setMutationError(null);
	};

	return (
		<>
			{mutationError && (
				<Container maxWidth="md" sx={{ mt: 2 }}>
					<Alert severity="error" onClose={handleCloseError}>
						{mutationError}
					</Alert>
				</Container>
			)}

			<Container maxWidth="md" sx={{ mt: 2, mb: 2 }}>
				<RegisterUser onRegisterSuccess={registerUser} onRegisterFailure={setMutationError} />
			</Container>
		</>
	);
}

export default RegisterUserView;
