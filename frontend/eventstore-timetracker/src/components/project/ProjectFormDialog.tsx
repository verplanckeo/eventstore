import React, { useState, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  FormControlLabel,
  Checkbox,
  Box,
  CircularProgress,
  Alert,
} from "@mui/material";
import type { 
    ReadProjectModel, 
    RegisterProjectRequest, 
    UpdateProjectRequest 
} from "../../interfaces/projects.interface";


interface ProjectFormDialogProps {
  open: boolean;
  project?: ReadProjectModel | null;
  onClose: () => void;
  onSubmit: (data: RegisterProjectRequest | UpdateProjectRequest) => Promise<void>;
  isLoading?: boolean;
}

interface FormErrors {
  name?: string;
  code?: string;
}

const ProjectFormDialog: React.FC<ProjectFormDialogProps> = ({
  open,
  project,
  onClose,
  onSubmit,
  isLoading = false,
}) => {
  const [formData, setFormData] = useState({
    name: "",
    code: "",
    billable: false,
  });

  const [errors, setErrors] = useState<FormErrors>({});
  const [submitError, setSubmitError] = useState<string | null>(null);

  const isEditMode = !!project;

  // Initialize form data when dialog opens or project changes
  useEffect(() => {
    if (project) {
      setFormData({
        name: project.name,
        code: project.code,
        billable: project.billable,
      });
    } else {
      setFormData({
        name: "",
        code: "",
        billable: false,
      });
    }
    setErrors({});
    setSubmitError(null);
  }, [project, open]);

  const handleInputChange = (field: keyof typeof formData) => (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const value = field === "billable" ? event.target.checked : event.target.value;
    
    setFormData({
      ...formData,
      [field]: value,
    });

    // Clear error for this field
    if (errors[field as keyof FormErrors]) {
      setErrors({
        ...errors,
        [field]: undefined,
      });
    }

    // Clear submit error
    if (submitError) {
      setSubmitError(null);
    }
  };

  const validateForm = (): boolean => {
    const newErrors: FormErrors = {};

    if (!formData.name.trim()) {
      newErrors.name = "Project name is required";
    } else if (formData.name.length > 100) {
      newErrors.name = "Project name must not exceed 100 characters";
    }

    if (!formData.code.trim()) {
      newErrors.code = "Project code is required";
    } else if (formData.code.length > 25) {
      newErrors.code = "Project code must not exceed 25 characters";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    setSubmitError(null);

    if (!validateForm()) {
      return;
    }

    try {
      await onSubmit(formData);
      handleClose();
    } catch (error) {
      setSubmitError(
        error instanceof Error ? error.message : "Failed to save project"
      );
    }
  };

  const handleClose = () => {
    setFormData({
      name: "",
      code: "",
      billable: false,
    });
    setErrors({});
    setSubmitError(null);
    onClose();
  };

  return (
    <Dialog open={open} onClose={handleClose} maxWidth="sm" fullWidth>
      <DialogTitle>
        {isEditMode ? "Edit Project" : "Create New Project"}
      </DialogTitle>

      <Box component="form" onSubmit={handleSubmit} noValidate>
        <DialogContent>
          {submitError && (
            <Alert severity="error" sx={{ mb: 2 }}>
              {submitError}
            </Alert>
          )}

          <TextField
            fullWidth
            label="Project Name"
            value={formData.name}
            onChange={handleInputChange("name")}
            error={!!errors.name}
            helperText={errors.name || "Maximum 100 characters"}
            margin="normal"
            required
            disabled={isLoading}
            autoFocus
            inputProps={{ maxLength: 100 }}
          />

          <TextField
            fullWidth
            label="Project Code"
            value={formData.code}
            onChange={handleInputChange("code")}
            error={!!errors.code}
            helperText={errors.code || "Maximum 25 characters (e.g., PROJ-001)"}
            margin="normal"
            required
            disabled={isLoading}
            inputProps={{ maxLength: 25 }}
          />

          <FormControlLabel
            control={
              <Checkbox
                checked={formData.billable}
                onChange={handleInputChange("billable")}
                disabled={isLoading}
              />
            }
            label="Billable Project"
            sx={{ mt: 2 }}
          />
        </DialogContent>

        <DialogActions sx={{ px: 3, pb: 2 }}>
          <Button onClick={handleClose} disabled={isLoading}>
            Cancel
          </Button>
          <Button
            type="submit"
            variant="contained"
            disabled={isLoading}
            startIcon={isLoading ? <CircularProgress size={20} /> : null}
          >
            {isLoading ? "Saving..." : isEditMode ? "Update" : "Create"}
          </Button>
        </DialogActions>
      </Box>
    </Dialog>
  );
};

export default ProjectFormDialog;