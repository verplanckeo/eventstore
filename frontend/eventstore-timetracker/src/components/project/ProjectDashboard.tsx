import React, { useState, useEffect, useCallback } from "react";
import {
  Container,
  Paper,
  Typography,
  Box,
  Button,
  Alert,
  CircularProgress,
} from "@mui/material";
import { Add, Refresh } from "@mui/icons-material";
import { useNotification } from "../notification/hooks/use-notification";
import type { ReadProjectModel, RegisterProjectRequest, UpdateProjectRequest } from "../../interfaces/projects.interface";
import { ProjectService } from "../../services/project.service";
import ProjectDeleteDialog from "./ProjectDeleteDialog";
import ProjectFormDialog from "./ProjectFormDialog";
import ProjectsTable from "./ProjectTable";

const Projects: React.FC = () => {
  const { showSuccess, showError } = useNotification();

  // State management
  const [projects, setProjects] = useState<ReadProjectModel[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [loadError, setLoadError] = useState<string | null>(null);

  // Dialog state
  const [isFormDialogOpen, setIsFormDialogOpen] = useState(false);
  const [isDeleteDialogOpen, setIsDeleteDialogOpen] = useState(false);
  const [selectedProject, setSelectedProject] = useState<ReadProjectModel | null>(null);

  // Load projects
  const loadProjects = useCallback(async () => {
    try {
      setIsLoading(true);
      setLoadError(null);
      const response = await ProjectService.getAllProjects();
      setProjects(response.projects || []);
    } catch (error) {
      const errorMessage =
        error instanceof Error ? error.message : "Failed to load projects";
      setLoadError(errorMessage);
      showError(errorMessage);
    } finally {
      setIsLoading(false);
    }
  }, [showError]);

  // Initial load
  useEffect(() => {
    loadProjects();
  }, [loadProjects]);

  // Handle create project
  const handleCreateClick = () => {
    setSelectedProject(null);
    setIsFormDialogOpen(true);
  };

  // Handle edit project
  const handleEditClick = (project: ReadProjectModel) => {
    setSelectedProject(project);
    setIsFormDialogOpen(true);
  };

  // Handle delete project click
  const handleDeleteClick = (project: ReadProjectModel) => {
    setSelectedProject(project);
    setIsDeleteDialogOpen(true);
  };

  // Handle form submit (create or update)
  const handleFormSubmit = async (
    data: RegisterProjectRequest | UpdateProjectRequest
  ) => {
    setIsSubmitting(true);
    try {
      if (selectedProject) {
        // Update existing project
        await ProjectService.updateProject(
          selectedProject.aggregateRootId,
          data as UpdateProjectRequest
        );
        showSuccess(`Project "${data.name}" updated successfully`);
      } else {
        // Create new project
        await ProjectService.registerProject(data as RegisterProjectRequest);
        showSuccess(`Project "${data.name}" created successfully`);
      }
      await loadProjects();
    } catch (error) {
      const errorMessage =
        error instanceof Error
          ? error.message
          : selectedProject
          ? "Failed to update project"
          : "Failed to create project";
      showError(errorMessage);
      throw error;
    } finally {
      setIsSubmitting(false);
    }
  };

  // Handle delete confirmation
  const handleDeleteConfirm = async () => {
    if (!selectedProject) return;

    setIsSubmitting(true);
    try {
      await ProjectService.deleteProject(selectedProject.aggregateRootId);
      showSuccess(`Project "${selectedProject.name}" deleted successfully`);
      await loadProjects();
    } catch (error) {
      const errorMessage =
        error instanceof Error ? error.message : "Failed to delete project";
      showError(errorMessage);
      throw error;
    } finally {
      setIsSubmitting(false);
    }
  };

  // Handle dialog close
  const handleFormDialogClose = () => {
    setIsFormDialogOpen(false);
    setSelectedProject(null);
  };

  const handleDeleteDialogClose = () => {
    setIsDeleteDialogOpen(false);
    setSelectedProject(null);
  };

  return (
    <Container maxWidth="lg" sx={{ py: 4 }}>
      {/* Header */}
      <Paper elevation={3} sx={{ p: 3, mb: 3 }}>
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            flexWrap: "wrap",
            gap: 2,
          }}
        >
          <Box>
            <Typography variant="h4" component="h1" gutterBottom>
              Projects Management
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Manage your projects, codes, and billing settings
            </Typography>
          </Box>
          <Box sx={{ display: "flex", gap: 2 }}>
            <Button
              variant="outlined"
              startIcon={<Refresh />}
              onClick={loadProjects}
              disabled={isLoading}
            >
              Refresh
            </Button>
            <Button
              variant="contained"
              startIcon={<Add />}
              onClick={handleCreateClick}
              disabled={isLoading}
            >
              New Project
            </Button>
          </Box>
        </Box>
      </Paper>

      {/* Error Alert */}
      {loadError && (
        <Alert severity="error" sx={{ mb: 3 }} onClose={() => setLoadError(null)}>
          {loadError}
        </Alert>
      )}

      {/* Loading State */}
      {isLoading && projects.length === 0 ? (
        <Box sx={{ display: "flex", justifyContent: "center", py: 8 }}>
          <CircularProgress />
        </Box>
      ) : (
        /* Projects Table */
        <ProjectsTable
          projects={projects}
          onEdit={handleEditClick}
          onDelete={handleDeleteClick}
          isLoading={isLoading}
        />
      )}

      {/* Form Dialog (Create/Edit) */}
      <ProjectFormDialog
        open={isFormDialogOpen}
        project={selectedProject}
        onClose={handleFormDialogClose}
        onSubmit={handleFormSubmit}
        isLoading={isSubmitting}
      />

      {/* Delete Confirmation Dialog */}
      <ProjectDeleteDialog
        open={isDeleteDialogOpen}
        project={selectedProject}
        onClose={handleDeleteDialogClose}
        onConfirm={handleDeleteConfirm}
        isLoading={isSubmitting}
      />
    </Container>
  );
};

export default Projects;