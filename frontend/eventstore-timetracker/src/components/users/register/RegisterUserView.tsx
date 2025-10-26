import React, { useState } from 'react';
import {
  Box,
  Button,
  TextField,
  Typography,
  Paper,
  Alert,
  CircularProgress,
  InputAdornment,
  IconButton,
} from '@mui/material';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import type { RegisterUserResponse } from '../../../interfaces/registeruserresponse.interface';

interface RegisterUserRequest {
  userName: string;
  password: string;
  firstName: string;
  lastName: string;
}

interface FormErrors {
  userName?: string;
  password?: string;
  firstName?: string;
  lastName?: string;
}

interface RegisterUserProps{
    onRegisterSuccess?: (user: RegisterUserResponse) => void;
    onRegisterFailure?: (error: string) => void;
}

const RegisterUserView: React.FC<RegisterUserProps> = ({ onRegisterSuccess, onRegisterFailure }) => {
  const [formData, setFormData] = useState<RegisterUserRequest>({
    userName: '',
    password: '',
    firstName: '',
    lastName: '',
  });

  const [errors, setErrors] = useState<FormErrors>({});
  const [showPassword, setShowPassword] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [successMessage, setSuccessMessage] = useState<string | null>(null);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const handleInputChange = (field: keyof RegisterUserRequest) => (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setFormData({
      ...formData,
      [field]: event.target.value,
    });
    // Clear error for this field when user starts typing
    if (errors[field]) {
      setErrors({
        ...errors,
        [field]: undefined,
      });
    }
  };

  const validateForm = (): boolean => {
    const newErrors: FormErrors = {};

    if (!formData.userName.trim()) {
      newErrors.userName = 'User name is required';
    }

    if (!formData.password.trim()) {
      newErrors.password = 'Password is required';
    } else if (formData.password.length < 8) {
      newErrors.password = 'Password must be at least 8 characters long';
    }

    if (!formData.firstName.trim()) {
      newErrors.firstName = 'First name is required';
    }

    if (!formData.lastName.trim()) {
      newErrors.lastName = 'Last name is required';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setSuccessMessage(null);
    setErrorMessage(null);

    if (!validateForm()) {
      return;
    }

    setIsLoading(true);

    try {
      const response = await fetch('/api/Users/register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData),
      });

      if (!response.ok) {
        throw new Error(`Registration failed: ${response.statusText}`);
      }

      const data: RegisterUserResponse = await response.json();
      
      setSuccessMessage(
        `User ${data.firstName} ${data.lastName} registered successfully!`
      );
      
      // Reset form
      setFormData({
        userName: '',
        password: '',
        firstName: '',
        lastName: '',
      });

      if (onRegisterSuccess) {
        onRegisterSuccess(data);
      }
    } catch (error) {
      setErrorMessage(
        error instanceof Error ? error.message : 'An error occurred during registration'
      );
      if (onRegisterFailure) {
        onRegisterFailure(error instanceof Error ? error.message : 'An error occurred during registration of a new user');
      }
    } finally {
        
      setIsLoading(false);
    }
  };

  const handleTogglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  return (
    <Box
      sx={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        minHeight: '100vh',
        bgcolor: 'background.default',
        p: 2,
      }}
    >
      <Paper
        elevation={3}
        sx={{
          p: 4,
          maxWidth: 500,
          width: '100%',
        }}
      >
        <Typography variant="h4" component="h1" gutterBottom align="center">
          Register User
        </Typography>

        {successMessage && (
          <Alert severity="success" sx={{ mb: 2 }}>
            {successMessage}
          </Alert>
        )}

        {errorMessage && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {errorMessage}
          </Alert>
        )}

        <Box component="form" onSubmit={handleSubmit} noValidate>
          <TextField
            fullWidth
            label="User Name"
            value={formData.userName}
            onChange={handleInputChange('userName')}
            error={!!errors.userName}
            helperText={errors.userName || 'e.g., overplan'}
            margin="normal"
            required
            disabled={isLoading}
          />

          <TextField
            fullWidth
            label="First Name"
            value={formData.firstName}
            onChange={handleInputChange('firstName')}
            error={!!errors.firstName}
            helperText={errors.firstName || 'e.g., Olivier'}
            margin="normal"
            required
            disabled={isLoading}
          />

          <TextField
            fullWidth
            label="Last Name"
            value={formData.lastName}
            onChange={handleInputChange('lastName')}
            error={!!errors.lastName}
            helperText={errors.lastName || 'e.g., Verplancke'}
            margin="normal"
            required
            disabled={isLoading}
          />

          <TextField
            fullWidth
            label="Password"
            type={showPassword ? 'text' : 'password'}
            value={formData.password}
            onChange={handleInputChange('password')}
            error={!!errors.password}
            helperText={errors.password || 'e.g., SuperSecureLongPassword'}
            margin="normal"
            required
            disabled={isLoading}
            InputProps={{
              endAdornment: (
                <InputAdornment position="end">
                  <IconButton
                    aria-label="toggle password visibility"
                    onClick={handleTogglePasswordVisibility}
                    edge="end"
                    disabled={isLoading}
                  >
                    {showPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              ),
            }}
          />

          <Button
            type="submit"
            fullWidth
            variant="contained"
            size="large"
            disabled={isLoading}
            sx={{ mt: 3, mb: 2 }}
          >
            {isLoading ? (
              <CircularProgress size={24} color="inherit" />
            ) : (
              'Register'
            )}
          </Button>
        </Box>
      </Paper>
    </Box>
  );
};

export default RegisterUserView;