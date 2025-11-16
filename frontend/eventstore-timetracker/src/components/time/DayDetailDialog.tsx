import React from "react";
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Typography,
  Box,
  Stack,
  IconButton,
  Chip,
  Divider,
  Tooltip,
  Paper,
} from "@mui/material";
import { Add, Edit, Delete, Close } from "@mui/icons-material";
import dayjs, { type Dayjs } from "dayjs";
import type { ReadTimeEntryModel } from "../../interfaces/timeentries.interface";
import { ActivityTypeLabels } from "../../interfaces/timeentries.interface";
import {
  formatDuration,
  calculateDuration,
  calculateTotalHours,
} from "../../utilities/timeentry.utils";

interface DayDetailDialogProps {
  open: boolean;
  date: Dayjs | null;
  entries: ReadTimeEntryModel[];
  onClose: () => void;
  onAddEntry: () => void;
  onEdit: (entry: ReadTimeEntryModel) => void;
  onDelete: (entry: ReadTimeEntryModel) => void;
  isLoading?: boolean;
}

const DayDetailDialog: React.FC<DayDetailDialogProps> = ({
  open,
  date,
  entries,
  onClose,
  onAddEntry,
  onEdit,
  onDelete,
  isLoading = false,
}) => {
  if (!date) return null;

  const totalHours = calculateTotalHours(entries);
  const sortedEntries = [...entries].sort((a, b) =>
    dayjs(a.from).isAfter(dayjs(b.from)) ? 1 : -1
  );

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth>
      <DialogTitle>
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
          }}
        >
          <Box>
            <Typography variant="h6">{date.format("dddd, MMMM D, YYYY")}</Typography>
            <Chip
              label={`Total: ${formatDuration(totalHours)}`}
              size="small"
              color="primary"
              sx={{ mt: 1 }}
            />
          </Box>
          <IconButton onClick={onClose} size="small">
            <Close />
          </IconButton>
        </Box>
      </DialogTitle>

      <DialogContent>
        {entries.length === 0 ? (
          <Box
            sx={{
              py: 4,
              textAlign: "center",
              color: "text.secondary",
            }}
          >
            <Typography variant="body1" gutterBottom>
              No time entries for this day
            </Typography>
            <Typography variant="body2">
              Click the button below to add your first entry
            </Typography>
          </Box>
        ) : (
          <Stack spacing={2}>
            {sortedEntries.map((entry) => (
              <Paper
                key={entry.aggregateRootId}
                elevation={1}
                sx={{
                  p: 2,
                  border: 1,
                  borderColor: "divider",
                  "&:hover": {
                    borderColor: "primary.main",
                    backgroundColor: "action.hover",
                  },
                }}
              >
                <Box
                  sx={{
                    display: "flex",
                    justifyContent: "space-between",
                    alignItems: "flex-start",
                    mb: 1,
                  }}
                >
                  <Box sx={{ flex: 1 }}>
                    <Typography variant="subtitle1" fontWeight={600}>
                      {entry.project.code}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                      {ActivityTypeLabels[entry.activityType]}
                    </Typography>
                  </Box>
                  <Box>
                    <Tooltip title="Edit">
                      <IconButton
                        size="small"
                        onClick={() => onEdit(entry)}
                        disabled={isLoading}
                      >
                        <Edit fontSize="small" />
                      </IconButton>
                    </Tooltip>
                    <Tooltip title="Delete">
                      <IconButton
                        size="small"
                        onClick={() => onDelete(entry)}
                        disabled={isLoading}
                        color="error"
                      >
                        <Delete fontSize="small" />
                      </IconButton>
                    </Tooltip>
                  </Box>
                </Box>

                <Divider sx={{ my: 1 }} />

                <Stack spacing={1}>
                  <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                    <Typography variant="body2" color="text.secondary">
                      Time:
                    </Typography>
                    <Typography variant="body2">
                      {dayjs(entry.from).format("HH:mm")} -{" "}
                      {dayjs(entry.until).format("HH:mm")}
                    </Typography>
                  </Box>

                  <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                    <Typography variant="body2" color="text.secondary">
                      Duration:
                    </Typography>
                    <Chip
                      label={formatDuration(
                        calculateDuration(entry.from, entry.until)
                      )}
                      size="small"
                      color="primary"
                      variant="outlined"
                    />
                  </Box>

                  {entry.comment && (
                    <>
                      <Divider sx={{ my: 0.5 }} />
                      <Box>
                        <Typography
                          variant="caption"
                          color="text.secondary"
                          display="block"
                          gutterBottom
                        >
                          Comment:
                        </Typography>
                        <Typography variant="body2" sx={{ fontStyle: "italic" }}>
                          {entry.comment}
                        </Typography>
                      </Box>
                    </>
                  )}
                </Stack>
              </Paper>
            ))}
          </Stack>
        )}
      </DialogContent>

      <DialogActions sx={{ px: 3, pb: 2 }}>
        <Button onClick={onClose}>Close</Button>
        <Button
          variant="contained"
          startIcon={<Add />}
          onClick={onAddEntry}
          disabled={isLoading}
        >
          Add Entry
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DayDetailDialog;