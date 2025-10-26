import React from "react";
import {
  Container,
  Paper,
  Typography,
  Box,
  Button,
  Card,
  CardContent,
  Grid,
} from "@mui/material";
import {
  AccessTime,
  Assignment,
  BarChart,
  ExitToApp,
} from "@mui/icons-material";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../hooks/use-auth";

const DashboardView: React.FC = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate("/login");
  };

  return (
    <Container maxWidth="lg" sx={{ py: 4 }}>
      <Paper elevation={3} sx={{ p: 4, mb: 4 }}>
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            mb: 3,
          }}
        >
          <Box>
            <Typography variant="h4" component="h1" gutterBottom>
              Welcome back, {user?.userName}!
            </Typography>
            <Typography variant="body1" color="text.secondary">
              User ID: {user?.id}
            </Typography>
          </Box>
          <Button
            variant="outlined"
            color="error"
            startIcon={<ExitToApp />}
            onClick={handleLogout}
          >
            Logout
          </Button>
        </Box>
      </Paper>

      <Grid container spacing={3}>
        <Grid size={{xs:12, md:4 }}>     
          <Card>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <AccessTime sx={{ fontSize: 40, color: "primary.main", mr: 2 }} />
                <Box>
                  <Typography variant="h6">Time Entries</Typography>
                  <Typography variant="body2" color="text.secondary">
                    Log your time
                  </Typography>
                </Box>
              </Box>
              <Button
                variant="contained"
                fullWidth
                onClick={() => navigate("/time-entries")}
              >
                Add Entry
              </Button>
            </CardContent>
          </Card>
        </Grid>

        <Grid size={{xs:12, md:4 }}>   
          <Card>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <Assignment sx={{ fontSize: 40, color: "primary.main", mr: 2 }} />
                <Box>
                  <Typography variant="h6">Projects</Typography>
                  <Typography variant="body2" color="text.secondary">
                    Manage projects
                  </Typography>
                </Box>
              </Box>
              <Button
                variant="contained"
                fullWidth
                onClick={() => navigate("/projects")}
              >
                View Projects
              </Button>
            </CardContent>
          </Card>
        </Grid>

        <Grid size={{xs:12, md:4 }}>   
          <Card>
            <CardContent>
              <Box sx={{ display: "flex", alignItems: "center", mb: 2 }}>
                <BarChart sx={{ fontSize: 40, color: "primary.main", mr: 2 }} />
                <Box>
                  <Typography variant="h6">Reports</Typography>
                  <Typography variant="body2" color="text.secondary">
                    View analytics
                  </Typography>
                </Box>
              </Box>
              <Button
                variant="contained"
                fullWidth
                onClick={() => navigate("/reports")}
              >
                View Reports
              </Button>
            </CardContent>
          </Card>
        </Grid>
      </Grid>
    </Container>
  );
};

export default DashboardView;