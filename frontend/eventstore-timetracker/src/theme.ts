// frontend/eventstore-timetracker/src/theme.ts
import { createTheme } from '@mui/material/styles';

const theme = createTheme({
  palette: {
    mode: 'dark',
    primary: {
      main: '#0077B6',
      dark: '#023E8A',
      light: '#00B4D8',
      contrastText: '#FFFFFF',
    },
    secondary: {
      main: '#00A896',
      dark: '#007F72',
      light: '#06D6A0',
      contrastText: '#FFFFFF',
    },
    background: {
      default: '#1A1A1A',
      paper: '#2D2D2D',
    },
    text: {
      primary: '#FFFFFF',
      secondary: '#B0B0B0',
      disabled: '#888888',
    },
    error: {
      main: '#C62828',
      light: '#FF5F52',
      dark: '#8E0000',
    },
    success: {
      main: '#4CAF50',
      light: '#81C784',
      dark: '#388E3C',
    },
    info: {
      main: '#2196F3',
      light: '#64B5F6',
      dark: '#1976D2',
    },
    warning: {
      main: '#FF9800',
      light: '#FFB74D',
      dark: '#F57C00',
    },
    divider: 'rgba(255, 255, 255, 0.12)',
  },
  typography: {
    fontFamily: "'Inter', -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif",
    h1: {
      fontWeight: 700,
    },
    h2: {
      fontWeight: 700,
    },
    h3: {
      fontWeight: 600,
    },
    h4: {
      fontWeight: 600,
    },
    h5: {
      fontWeight: 600,
    },
    h6: {
      fontWeight: 600,
    },
    button: {
      fontWeight: 600,
      textTransform: 'none',
    },
  },
  shape: {
    borderRadius: 8,
  },
  shadows: [
    'none',
    '0 2px 8px rgba(0, 119, 182, 0.15)',
    '0 4px 12px rgba(0, 0, 0, 0.3)',
    '0 6px 20px rgba(0, 119, 182, 0.3)',
    '0 8px 32px rgba(0, 0, 0, 0.4)',
    '0 10px 40px rgba(0, 119, 182, 0.35)',
    '0 12px 48px rgba(0, 0, 0, 0.5)',
    '0 14px 56px rgba(0, 119, 182, 0.4)',
    '0 16px 64px rgba(0, 0, 0, 0.6)',
    '0 18px 72px rgba(0, 119, 182, 0.45)',
    '0 20px 80px rgba(0, 0, 0, 0.7)',
    '0 22px 88px rgba(0, 119, 182, 0.5)',
    '0 24px 96px rgba(0, 0, 0, 0.8)',
    '0 26px 104px rgba(0, 119, 182, 0.55)',
    '0 28px 112px rgba(0, 0, 0, 0.9)',
    '0 30px 120px rgba(0, 119, 182, 0.6)',
    '0 32px 128px rgba(0, 0, 0, 1)',
    '0 34px 136px rgba(0, 119, 182, 0.65)',
    '0 36px 144px rgba(0, 0, 0, 1)',
    '0 38px 152px rgba(0, 119, 182, 0.7)',
    '0 40px 160px rgba(0, 0, 0, 1)',
    '0 42px 168px rgba(0, 119, 182, 0.75)',
    '0 44px 176px rgba(0, 0, 0, 1)',
    '0 46px 184px rgba(0, 119, 182, 0.8)',
    '0 48px 192px rgba(0, 0, 0, 1)',
  ],
  components: {
    MuiCard: {
      styleOverrides: {
        root: {
          borderRadius: 16,
          border: '1px solid #404040',
          backgroundColor: '#3A3A3A',
        },
      },
    },
    MuiPaper: {
      styleOverrides: {
        root: {
          backgroundImage: 'none',
          backgroundColor: '#2D2D2D',
          borderRadius: 16,
        },
        elevation3: {
          boxShadow: '0 8px 32px rgba(0, 0, 0, 0.4)',
        },
      },
    },
    MuiButton: {
      styleOverrides: {
        root: {
          borderRadius: 8,
          textTransform: 'none',
          fontWeight: 600,
          padding: '10px 24px',
          boxShadow: '0 2px 8px rgba(0, 119, 182, 0.3)',
          transition: 'all 0.3s ease',
          '&:hover': {
            transform: 'translateY(-2px)',
            boxShadow: '0 4px 16px rgba(0, 119, 182, 0.4)',
          },
          '&:active': {
            transform: 'translateY(0)',
          },
        },
        contained: {
          background: 'linear-gradient(135deg, #0077B6 0%, #00A896 100%)',
          '&:hover': {
            background: 'linear-gradient(135deg, #00A896 0%, #023E8A 100%)',
          },
        },
        outlined: {
          borderColor: '#0077B6',
          color: '#00B4D8',
          '&:hover': {
            borderColor: '#00A896',
            backgroundColor: 'rgba(0, 119, 182, 0.1)',
          },
        },
      },
    },
    MuiTextField: {
      styleOverrides: {
        root: {
          '& .MuiOutlinedInput-root': {
            backgroundColor: '#2D2D2D',
            borderRadius: 8,
            transition: 'all 0.3s ease',
            '& fieldset': {
              borderColor: '#404040',
            },
            '&:hover fieldset': {
              borderColor: '#0077B6',
            },
            '&.Mui-focused fieldset': {
              borderColor: '#0077B6',
              boxShadow: '0 0 0 3px rgba(0, 119, 182, 0.15)',
            },
          },
        },
      },
    },
    MuiInputLabel: {
      styleOverrides: {
        root: {
          color: '#B0B0B0',
          '&.Mui-focused': {
            color: '#00B4D8',
          },
        },
      },
    },
    MuiAlert: {
      styleOverrides: {
        root: {
          borderRadius: 8,
        },
        standardSuccess: {
          backgroundColor: 'rgba(76, 175, 80, 0.15)',
          border: '1px solid rgba(76, 175, 80, 0.3)',
          color: '#81C784',
        },
        standardError: {
          backgroundColor: 'rgba(198, 40, 40, 0.15)',
          border: '1px solid #C62828',
          color: '#FF5F52',
        },
        standardInfo: {
          backgroundColor: 'rgba(33, 150, 243, 0.15)',
          border: '1px solid rgba(33, 150, 243, 0.3)',
          color: '#64B5F6',
        },
        standardWarning: {
          backgroundColor: 'rgba(255, 152, 0, 0.15)',
          border: '1px solid rgba(255, 152, 0, 0.3)',
          color: '#FFB74D',
        },
      },
    },
    MuiTableHead: {
      styleOverrides: {
        root: {
          '& .MuiTableCell-root': {
            backgroundColor: '#0077B6',
            color: '#FFFFFF',
            fontWeight: 600,
          },
        },
      },
    },
    MuiTableBody: {
      styleOverrides: {
        root: {
          '& .MuiTableRow-root': {
            '&:hover': {
              backgroundColor: 'rgba(0, 119, 182, 0.1)',
            },
          },
          '& .MuiTableCell-root': {
            borderBottom: '1px solid #404040',
            color: '#B0B0B0',
          },
        },
      },
    },
    MuiIconButton: {
      styleOverrides: {
        root: {
          color: '#B0B0B0',
          '&:hover': {
            backgroundColor: 'rgba(0, 119, 182, 0.1)',
            color: '#00B4D8',
          },
        },
      },
    },
    MuiChip: {
      styleOverrides: {
        root: {
          borderRadius: 8,
        },
        filled: {
          backgroundColor: '#3A3A3A',
          border: '1px solid #404040',
        },
      },
    },
    MuiCircularProgress: {
      styleOverrides: {
        root: {
          color: '#0077B6',
        },
      },
    },
    MuiBottomNavigation: {
      styleOverrides: {
        root: {
          backgroundColor: '#2D2D2D',
          borderTop: '1px solid #404040',
        },
      },
    },
    MuiBottomNavigationAction: {
      styleOverrides: {
        root: {
          color: '#B0B0B0',
          transition: 'all 0.3s ease',
          '&:hover': {
            color: '#B3E5FC',
            backgroundColor: 'rgba(0, 119, 182, 0.05)',
          },
          '&.Mui-selected': {
            color: '#00B4D8',
            '& .MuiBottomNavigationAction-label': {
              fontSize: '0.75rem',
              fontWeight: 600,
              color: '#00B4D8',
            },
          },
          '& .MuiBottomNavigationAction-label': {
            color: 'inherit',
            transition: 'all 0.3s ease',
          },
        },
      },
    },
  },
});

export default theme;