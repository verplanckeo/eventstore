import React, { useState, useEffect } from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  TextField,
  MenuItem,
  Box,
  CircularProgress,
  Alert,
  FormControl,
  InputLabel,
  Select,
  Grid,
  type SelectChangeEvent,
} from "@mui/material";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import dayjs, { type Dayjs } from "dayjs";
import type {
  ReadTimeEntryModel,
  RegisterTimeEntryRequest,
  UpdateTimeEntryRequest,
  ActivityType,
} from "../../interfaces/timeentries.interface";
import { ActivityTypeLabels } from "../../interfaces/timeentries.interface";
import type { ReadProjectModel } from "../../interfaces/projects.interface";
import { calculateDuration, formatDuration, validateDuration } from "../../utilities/timeentry.utils";

interface TimeEntryFormDialogProps {
  open: boolean;
  timeEntry?: ReadTimeEntryModel | null;
  initialDate?: Dayjs;
  projects: ReadProjectModel[];
  currentUserId: string;
  onClose: () => void;
  onSubmit: (
    data: RegisterTimeEntryRequest | UpdateTimeEntryRequest
  ) => Promise<void>;
  isLoading?: boolean;
}

interface FormErrors {
  from?: string;
  until?: string;
  projectId?: string;
  activityType?: string;
  comment?: string;
}

const TimeEntryFormDialog: React.FC<TimeEntryFormDialogProps> = ({
  open,
  timeEntry,
  initialDate,
  projects,
  currentUserId,
  onClose,
  onSubmit,
  isLoading = false,
}) => {
  const [formData, setFormData] = useState<{
    from: Dayjs | null;
    until: Dayjs | null;
    projectId: string;
    activityType: ActivityType;
    comment: string;
  }>({
    from: null,
    until: null,
    projectId: "",
    activityType: 99,
    comment: "",
  });

  const [errors, setErrors] = useState<FormErrors>({});
  const [submitError, setSubmitError] = useState<string | null>(null);
  const [calculatedDuration, setCalculatedDuration] = useState<number>(0);

  const isEditMode = !!timeEntry;

  // Initialize form data when dialog opens or timeEntry changes
  useEffect(() => {
    if (timeEntry) {
      setFormData({
        from: dayjs(timeEntry.from),
        until: dayjs(timeEntry.until),
        projectId: timeEntry.project.id,
        activityType: timeEntry.activityType,
        comment: timeEntry.comment || "",
      });
    } else {
      const defaultFrom = initialDate || dayjs();
      setFormData({
        from: defaultFrom.hour(9).minute(0).second(0),
        until: defaultFrom.hour(17).minute(0).second(0),
        projectId: "",
        activityType: 99,
        comment: "",
      });
    }
    setErrors({});
    setSubmitError(null);
  }, [timeEntry, open, initialDate]);

  // Calculate duration whenever from/until changes
  useEffect(() => {
    if (formData.from && formData.until) {
      const duration = calculateDuration(
        formData.from.toISOString(),
        formData.until.toISOString()
      );
      setCalculatedDuration(duration);
    } else {
      setCalculatedDuration(0);
    }
  }, [formData.from, formData.until]);

  const handleFromChange = (newValue: Dayjs | null) => {
    setFormData((prev) => ({
      ...prev,
      from: newValue,
    }));

    if (errors.from) {
      setErrors((prev) => ({ ...prev, from: undefined }));
    }
    if (submitError) {
      setSubmitError(null);
    }
  };

  const handleUntilChange = (newValue: Dayjs | null) => {
    setFormData((prev) => ({
      ...prev,
      until: newValue,
    }));

    if (errors.until) {
      setErrors((prev) => ({ ...prev, until: undefined }));
    }
    if (submitError) {
      setSubmitError(null);
    }
  };

  const handleProjectChange = (event: SelectChangeEvent<string>) => {
    setFormData((prev) => ({
      ...prev,
      projectId: event.target.value,
    }));

    if (errors.projectId) {
      setErrors((prev) => ({ ...prev, projectId: undefined }));
    }
    if (submitError) {
      setSubmitError(null);
    }
  };

  const handleActivityTypeChange = (
    event: SelectChangeEvent<ActivityType>
  ) => {
    setFormData((prev) => ({
      ...prev,
      activityType: event.target.value as ActivityType,
    }));

    if (errors.activityType) {
      setErrors((prev) => ({ ...prev, activityType: undefined }));
    }
    if (submitError) {
      setSubmitError(null);
    }
  };

  const handleCommentChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setFormData((prev) => ({
      ...prev,
      comment: event.target.value,
    }));

    if (errors.comment) {
      setErrors((prev) => ({ ...prev, comment: undefined }));
    }
    if (submitError) {
      setSubmitError(null);
    }
  };

  const validateForm = (): boolean => {
    const newErrors: FormErrors = {};

    if (!formData.from) {
      newErrors.from = "Start date/time is required";
    }

    if (!formData.until) {
      newErrors.until = "End date/time is required";
    }

    if (formData.from && formData.until) {
      const durationError = validateDuration(
        formData.from.toISOString(),
        formData.until.toISOString()
      );
      if (durationError) {
        newErrors.until = durationError;
      }
    }

    if (!formData.projectId) {
      newErrors.projectId = "Project is required";
    }

    if (!formData.activityType) {
      newErrors.activityType = "Activity type is required";
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

    if (!formData.from || !formData.until || !formData.activityType) {
      return;
    }

    try {
      const requestData = {
        from: formData.from.toISOString(),
        until: formData.until.toISOString(),
        userId: currentUserId,
        projectId: formData.projectId,
        activityType: formData.activityType,
        comment: formData.comment || undefined,
      };

      await onSubmit(requestData);
      handleClose();
    } catch (error) {
      setSubmitError(
        error instanceof Error ? error.message : "Failed to save time entry"
      );
    }
  };

  const handleClose = () => {
    setFormData({
      from: null,
      until: null,
      projectId: "",
      activityType: 99,
      comment: "",
    });
    setErrors({});
    setSubmitError(null);
    onClose();
  };

  // Filter active projects
  const activeProjects = projects.filter((p) => !p.isRemoved);

  return (
    <Dialog open={open} onClose={handleClose} maxWidth="md" fullWidth>
      <DialogTitle>
        {isEditMode ? "Edit Time Entry" : "Log Time Entry"}
      </DialogTitle>

      <Box component="form" onSubmit={handleSubmit} noValidate>
        <DialogContent>
          {submitError && (
            <Alert severity="error" sx={{ mb: 2 }}>
              {submitError}
            </Alert>
          )}

          <Grid container spacing={2}>
            <Grid size={{xs:12, sm:6 }}>       
              <DateTimePicker
                label="Start Time *"
                value={formData.from}
                onChange={handleFromChange}
                disabled={isLoading}
                slotProps={{
                  textField: {
                    fullWidth: true,
                    error: !!errors.from,
                    helperText: errors.from,
                  },
                }}
              />
            </Grid>

            <Grid size={{xs:12, sm:6 }}>       
              <DateTimePicker
                label="End Time *"
                value={formData.until}
                onChange={handleUntilChange}
                disabled={isLoading}
                slotProps={{
                  textField: {
                    fullWidth: true,
                    error: !!errors.until,
                    helperText: errors.until,
                  },
                }}
              />
            </Grid>

            <Grid size={{xs:12}}>       
              {calculatedDuration > 0 && (
                <Alert severity="info" sx={{ mt: 1 }}>
                  Duration: {formatDuration(calculatedDuration)}
                </Alert>
              )}
            </Grid>

            <Grid size={{xs:12}}>       
              <FormControl fullWidth error={!!errors.projectId}>
                <InputLabel>Project *</InputLabel>
                <Select
                  value={formData.projectId}
                  onChange={handleProjectChange}
                  label="Project *"
                  disabled={isLoading}
                >
                  {activeProjects.map((project) => (
                    <MenuItem key={project.aggregateRootId} value={project.aggregateRootId}>
                      {project.name} ({project.code})
                    </MenuItem>
                  ))}
                </Select>
                {errors.projectId && (
                  <Box sx={{ color: "error.main", fontSize: "0.75rem", mt: 0.5 }}>
                    {errors.projectId}
                  </Box>
                )}
              </FormControl>
            </Grid>

            <Grid size={{xs:12}}>       
              <FormControl fullWidth error={!!errors.activityType}>
                <InputLabel>Activity Type *</InputLabel>
                <Select
                  value={formData.activityType}
                  onChange={handleActivityTypeChange}
                  label="Activity Type *"
                  disabled={isLoading}
                >
                  {Object.entries(ActivityTypeLabels).map(([value, label]) => (
                    <MenuItem key={value} value={value}>
                      {label}
                    </MenuItem>
                  ))}
                </Select>
                {errors.activityType && (
                  <Box sx={{ color: "error.main", fontSize: "0.75rem", mt: 0.5 }}>
                    {errors.activityType}
                  </Box>
                )}
              </FormControl>
            </Grid>

            <Grid size={{xs:12}}>       
              <TextField
                fullWidth
                label="Comment"
                value={formData.comment}
                onChange={handleCommentChange}
                error={!!errors.comment}
                helperText={errors.comment || "Optional description of your work"}
                multiline
                rows={3}
                disabled={isLoading}
              />
            </Grid>
          </Grid>
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
            {isLoading ? "Saving..." : isEditMode ? "Update" : "Log Time"}
          </Button>
        </DialogActions>
      </Box>
    </Dialog>
  );
};

export default TimeEntryFormDialog;