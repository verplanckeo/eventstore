import React from "react";
import {
  Paper,
  Typography,
  Box,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  IconButton,
  Chip,
  Tooltip,
  Stack,
} from "@mui/material";
import { Edit, Delete, Add } from "@mui/icons-material";
import dayjs, { type Dayjs } from "dayjs";
import type { ReadTimeEntryModel } from "../../interfaces/timeentries.interface";
import { ActivityTypeLabels } from "../../interfaces/timeentries.interface";
import type { WeekSummary } from "../../interfaces/timeentries.interface";
import {
  formatDuration,
  calculateDuration,
  isToday,
  getWeekdays,
} from "../../utilities/timeentry.utils";

interface WeekViewProps {
  weekSummary: WeekSummary;
  onEdit: (entry: ReadTimeEntryModel) => void;
  onDelete: (entry: ReadTimeEntryModel) => void;
  onAddEntry: (date: Dayjs) => void;
  isLoading?: boolean;
}

const WeekView: React.FC<WeekViewProps> = ({
  weekSummary,
  onEdit,
  onDelete,
  onAddEntry,
  isLoading = false,
}) => {
  const weekdays = getWeekdays();

  const getDayColor = (date: string): string => {
    if (isToday(date)) {
      return "primary.light";
    }
    const day = dayjs(date).day();
    // Weekend (Saturday: 6, Sunday: 0)
    if (day === 0 || day === 6) {
      return "text.disabled";
    }
    return "text.primary";
  };

  return (
    <TableContainer component={Paper}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Time</TableCell>
            {weekSummary.dailySummaries.map((daySummary, index) => {
              const date = dayjs(daySummary.date);
              const today = isToday(daySummary.date);
              
              return (
                <TableCell
                  key={daySummary.date}
                  align="center"
                  sx={{
                    minWidth: 120,
                    backgroundColor: today ? "action.selected" : "inherit",
                  }}
                >
                  <Stack spacing={0.5} alignItems="center">
                    <Typography
                      variant="caption"
                      sx={{ color: getDayColor(daySummary.date) }}
                    >
                      {weekdays[index]}
                    </Typography>
                    <Typography
                      variant="body2"
                      fontWeight={today ? 700 : 500}
                      sx={{ color: getDayColor(daySummary.date) }}
                    >
                      {date.format("MMM D")}
                    </Typography>
                    <Chip
                      label={formatDuration(daySummary.totalHours)}
                      size="small"
                      variant={today ? "filled" : "outlined"}
                      color={daySummary.totalHours > 0 ? "primary" : "default"}
                    />
                    <Tooltip title="Add time entry">
                      <IconButton
                        size="small"
                        onClick={() => onAddEntry(date)}
                        disabled={isLoading}
                      >
                        <Add fontSize="small" />
                      </IconButton>
                    </Tooltip>
                  </Stack>
                </TableCell>
              );
            })}
            <TableCell align="right">
              <Typography variant="body2" fontWeight={600}>
                Week Total
              </Typography>
              <Chip
                label={formatDuration(weekSummary.totalHours)}
                size="small"
                color="primary"
              />
            </TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {/* Group entries by time slot */}
          {getTimeSlots(weekSummary).map((slot, slotIndex) => (
            <TableRow key={slotIndex}>
              <TableCell sx={{ verticalAlign: "top", minWidth: 80 }}>
                <Typography variant="body2" color="text.secondary">
                  {slot.timeLabel}
                </Typography>
              </TableCell>
              {weekSummary.dailySummaries.map((daySummary) => {
                const entriesInSlot = slot.entries.filter(
                  (e) =>
                    dayjs(e.from).format("YYYY-MM-DD") === daySummary.date
                );

                return (
                  <TableCell
                    key={daySummary.date}
                    sx={{
                      verticalAlign: "top",
                      backgroundColor: isToday(daySummary.date)
                        ? "action.hover"
                        : "inherit",
                    }}
                  >
                    {entriesInSlot.map((entry) => (
                      <Box
                        key={entry.aggregateRootId}
                        sx={{
                          mb: 1,
                          p: 1,
                          borderRadius: 1,
                          backgroundColor: "background.default",
                          border: 1,
                          borderColor: "divider",
                          "&:hover": {
                            borderColor: "primary.main",
                          },
                        }}
                      >
                        <Stack spacing={0.5}>
                          <Box
                            sx={{
                              display: "flex",
                              justifyContent: "space-between",
                              alignItems: "center",
                            }}
                          >
                            <Typography variant="caption" fontWeight={600}>
                              {entry.project.code}
                            </Typography>
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
                          <Typography variant="caption" color="text.secondary">
                            {ActivityTypeLabels[entry.activityType]}
                          </Typography>
                          <Typography variant="caption" color="text.secondary">
                            {dayjs(entry.from).format("HH:mm")} -{" "}
                            {dayjs(entry.until).format("HH:mm")}
                          </Typography>
                          <Chip
                            label={formatDuration(
                              calculateDuration(entry.from, entry.until)
                            )}
                            size="small"
                            variant="outlined"
                          />
                          {entry.comment && (
                            <Typography
                              variant="caption"
                              sx={{
                                fontStyle: "italic",
                                color: "text.secondary",
                                display: "-webkit-box",
                                WebkitLineClamp: 2,
                                WebkitBoxOrient: "vertical",
                                overflow: "hidden",
                              }}
                            >
                              {entry.comment}
                            </Typography>
                          )}
                        </Stack>
                      </Box>
                    ))}
                  </TableCell>
                );
              })}
              <TableCell />
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

// Helper function to organize entries into time slots
interface TimeSlot {
  timeLabel: string;
  entries: ReadTimeEntryModel[];
}

function getTimeSlots(weekSummary: WeekSummary): TimeSlot[] {
  const allEntries = weekSummary.dailySummaries.flatMap((day) => day.entries);

  if (allEntries.length === 0) {
    return [];
  }

  // Find the earliest and latest times
  let earliestHour = 24;
  let latestHour = 0;

  allEntries.forEach((entry) => {
    const fromHour = dayjs(entry.from).hour();
    const untilHour = dayjs(entry.until).hour();
    earliestHour = Math.min(earliestHour, fromHour);
    latestHour = Math.max(latestHour, untilHour);
  });

  // Create time slots
  const slots: TimeSlot[] = [];
  for (let hour = earliestHour; hour <= latestHour; hour++) {
    const entriesInSlot = allEntries.filter((entry) => {
      const entryHour = dayjs(entry.from).hour();
      return entryHour === hour;
    });

    if (entriesInSlot.length > 0) {
      slots.push({
        timeLabel: `${hour.toString().padStart(2, "0")}:00`,
        entries: entriesInSlot,
      });
    }
  }

  return slots;
}

export default WeekView;