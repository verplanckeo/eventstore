import React from "react";
import {
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Paper,
  IconButton,
  Chip,
  Box,
  Typography,
  Tooltip,
} from "@mui/material";
import { Edit, Delete, CheckCircle, Cancel } from "@mui/icons-material";
import type { ReadProjectModel } from "../../interfaces/projects.interface";

interface ProjectsTableProps {
  projects: ReadProjectModel[];
  onEdit: (project: ReadProjectModel) => void;
  onDelete: (project: ReadProjectModel) => void;
  isLoading?: boolean;
}

const ProjectsTable: React.FC<ProjectsTableProps> = ({
  projects,
  onEdit,
  onDelete,
  isLoading = false,
}) => {
  // Filter out removed projects
  const activeProjects = projects.filter((p) => !p.isRemoved);

  if (activeProjects.length === 0) {
    return (
      <Paper sx={{ p: 4, textAlign: "center" }}>
        <Typography variant="h6" color="text.secondary">
          No projects found
        </Typography>
        <Typography variant="body2" color="text.secondary" sx={{ mt: 1 }}>
          Click the "New Project" button to create your first project
        </Typography>
      </Paper>
    );
  }

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
            <TableCell>Code</TableCell>
            <TableCell align="center">Billable</TableCell>
            <TableCell align="center">Version</TableCell>
            <TableCell align="right">Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {activeProjects.map((project) => (
            <TableRow
              key={project.aggregateRootId}
              sx={{
                "&:last-child td, &:last-child th": { border: 0 },
                opacity: isLoading ? 0.5 : 1,
              }}
            >
              <TableCell>
                <Typography variant="body1" fontWeight={600}>
                  {project.name}
                </Typography>
              </TableCell>
              <TableCell>
                <Chip
                  label={project.code}
                  size="small"
                  variant="outlined"
                  sx={{ fontFamily: "monospace" }}
                />
              </TableCell>
              <TableCell align="center">
                {project.billable ? (
                  <CheckCircle color="success" />
                ) : (
                  <Cancel color="disabled" />
                )}
              </TableCell>
              <TableCell align="center">
                <Chip label={`v${project.version}`} size="small" />
              </TableCell>
              <TableCell align="right">
                <Box sx={{ display: "flex", gap: 1, justifyContent: "flex-end" }}>
                  <Tooltip title="Edit project">
                    <IconButton
                      size="small"
                      onClick={() => onEdit(project)}
                      disabled={isLoading}
                      color="primary"
                    >
                      <Edit />
                    </IconButton>
                  </Tooltip>
                  <Tooltip title="Delete project">
                    <IconButton
                      size="small"
                      onClick={() => onDelete(project)}
                      disabled={isLoading}
                      color="error"
                    >
                      <Delete />
                    </IconButton>
                  </Tooltip>
                </Box>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default ProjectsTable;