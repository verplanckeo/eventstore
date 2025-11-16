import React from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Typography,
  CircularProgress,
  Alert,
  Box,
} from "@mui/material";
import { Warning } from "@mui/icons-material";
import dayjs from "dayjs";
import type { ReadTimeEntryModel } from "../../interfaces/timeentries.interface";
import { ActivityTypeLabels } from "../../interfaces/timeentries.interface";
import { formatDuration, calculateDuration } from "../../utilities/timeentry.utils";

interface TimeEntryDeleteDialogProps {
  open: boolean;
  timeEntry: ReadTimeEntryModel | null;
  onClose: () => void;
  onConfirm: () => Promise<void>;
  isLoading?: boolean;
}

const TimeEntryDeleteDialog: React.FC<TimeEntryDeleteDialogProps> = ({
  open,
  timeEntry,
  onClose,
  onConfirm,
  isLoading = false,
}) => {
  const [error, setError] = React.useState<string | null>(null);

  const handleConfirm = async () => {
    setError(null);
    try {
      await onConfirm();
      onClose();
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to delete time entry");
    }
  };

  const handleClose = () => {
    setError(null);
    onClose();
  };

  if (!timeEntry) return null;

  return (
    <Dialog open={open} onClose={handleClose} maxWidth="sm" fullWidth>
      <DialogTitle sx={{ display: "flex", alignItems: "center", gap: 1 }}>
        <Warning color="error" />
        Delete Time Entry
      </DialogTitle>

      <DialogContent>
        {error && (
          <Alert severity="error" sx={{ mb: 2 }}>
            {error}
          </Alert>
        )}

        <Typography variant="body1" gutterBottom>
          Are you sure you want to delete this time entry?
        </Typography>

        <Box
          sx={{
            mt: 2,
            p: 2,
            bgcolor: "background.default",
            borderRadius: 1,
            border: 1,
            borderColor: "divider",
          }}
        >
          <Typography variant="body2" gutterBottom>
            <strong>Date:</strong> {dayjs(timeEntry.from).format("MMMM D, YYYY")}
          </Typography>
          <Typography variant="body2" gutterBottom>
            <strong>Time:</strong> {dayjs(timeEntry.from).format("HH:mm")} -{" "}
            {dayjs(timeEntry.until).format("HH:mm")}
          </Typography>
          <Typography variant="body2" gutterBottom>
            <strong>Duration:</strong>{" "}
            {formatDuration(calculateDuration(timeEntry.from, timeEntry.until))}
          </Typography>
          <Typography variant="body2" gutterBottom>
            <strong>Project:</strong> {timeEntry.project.code}
          </Typography>
          <Typography variant="body2" gutterBottom>
            <strong>Activity:</strong> {ActivityTypeLabels[timeEntry.activityType]}
          </Typography>
          {timeEntry.comment && (
            <Typography variant="body2">
              <strong>Comment:</strong> {timeEntry.comment}
            </Typography>
          )}
        </Box>

        <Typography variant="body2" color="text.secondary" sx={{ mt: 2 }}>
          This action cannot be undone.
        </Typography>
      </DialogContent>

      <DialogActions sx={{ px: 3, pb: 2 }}>
        <Button onClick={handleClose} disabled={isLoading}>
          Cancel
        </Button>
        <Button
          onClick={handleConfirm}
          variant="contained"
          color="error"
          disabled={isLoading}
          startIcon={isLoading ? <CircularProgress size={20} /> : null}
        >
          {isLoading ? "Deleting..." : "Delete"}
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default TimeEntryDeleteDialog;