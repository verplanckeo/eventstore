import React from "react";
import {
  Paper,
  Typography,
  Box,
  Grid,
  Chip,
  Stack,
  IconButton,
  Tooltip,
  Card,
  CardContent,
} from "@mui/material";
import { Add } from "@mui/icons-material";
import dayjs, { type Dayjs } from "dayjs";
import type {
  MonthSummary,
  ReadTimeEntryModel,
} from "../../interfaces/timeentries.interface";
import {
  formatDuration,
  calculateDuration,
  isToday,
  isFuture,
} from "../../utilities/timeentry.utils";

interface MonthViewProps {
  monthSummary: MonthSummary;
  onEdit: (entry: ReadTimeEntryModel) => void;
  onDelete: (entry: ReadTimeEntryModel) => void;
  onAddEntry: (date: Dayjs) => void;
  onDayClick: (date: Dayjs) => void;
  isLoading?: boolean;
}

const MonthView: React.FC<MonthViewProps> = ({
  monthSummary,
  onEdit,
  onDelete,
  onAddEntry,
  onDayClick,
  isLoading = false,
}) => {
  const firstDayOfMonth = dayjs()
    .year(monthSummary.year)
    .month(monthSummary.month - 1)
    .date(1);

  // Get the day of week for the first day (0 = Sunday, 1 = Monday, etc.)
  const startDay = firstDayOfMonth.day();
  
  // Adjust to start from Monday (1 = Monday, 0 = Sunday)
  const startOffset = startDay === 0 ? 6 : startDay - 1;

  // Calendar grid includes empty cells at the start
  const calendarDays: (Dayjs | null)[] = [];

  // Add empty cells for days before the first day of the month
  for (let i = 0; i < startOffset; i++) {
    calendarDays.push(null);
  }

  // Add all days of the month
  const daysInMonth = firstDayOfMonth.daysInMonth();
  for (let day = 1; day <= daysInMonth; day++) {
    calendarDays.push(firstDayOfMonth.date(day));
  }

  const weekdays = ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];

  const getDaySummary = (date: Dayjs) => {
    const dateStr = date.format("YYYY-MM-DD");
    return monthSummary.dailySummaries.find((s) => s.date === dateStr);
  };

  const getDayColor = (date: Dayjs): { bg: string; text: string } => {
    if (isToday(date)) {
      return { bg: "primary.main", text: "primary.contrastText" };
    }
    if (isFuture(date)) {
      return { bg: "action.disabledBackground", text: "text.disabled" };
    }
    const day = date.day();
    if (day === 0 || day === 6) {
      return { bg: "action.hover", text: "text.secondary" };
    }
    return { bg: "background.default", text: "text.primary" };
  };

  return (
    <Paper sx={{ p: 2 }}>
      {/* Month header with total */}
      <Box sx={{ mb: 3, display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <Typography variant="h6">
          {firstDayOfMonth.format("MMMM YYYY")}
        </Typography>
        <Chip
          label={`Total: ${formatDuration(monthSummary.totalHours)}`}
          color="primary"
          variant="outlined"
        />
      </Box>

      {/* Weekday headers */}
      <Grid container spacing={1} sx={{ mb: 1 }}>
        {weekdays.map((day) => (
          <Grid size={{xs:12/7}} key={day}>
            <Typography
              variant="caption"
              fontWeight={600}
              align="center"
              display="block"
              color="text.secondary"
            >
              {day}
            </Typography>
          </Grid>
        ))}
      </Grid>

      {/* Calendar grid */}
      <Grid container spacing={1}>
        {calendarDays.map((date, index) => {
          if (!date) {
            // Empty cell
            return (
              <Grid size={{xs:12/7}} key={`empty-${index}`}>
                <Box sx={{ height: 120 }} />
              </Grid>
            );
          }

          const daySummary = getDaySummary(date);
          const colors = getDayColor(date);
          const today = isToday(date);

          return (
            <Grid size={{xs:12/7}} key={date.format("YYYY-MM-DD")}>
              <Card
                sx={{
                  height: 120,
                  display: "flex",
                  flexDirection: "column",
                  border: today ? 2 : 1,
                  borderColor: today ? "primary.main" : "divider",
                  backgroundColor: colors.bg,
                  cursor: isLoading ? "default" : "pointer",
                  transition: "all 0.2s ease",
                  "&:hover": {
                    transform: isLoading ? "none" : "translateY(-2px)",
                    boxShadow: isLoading ? "none" : 3,
                  },
                }}
                onClick={() => !isLoading && onDayClick(date)}
              >
                <CardContent sx={{ flex: 1, p: 1, "&:last-child": { pb: 1 }, width: "100%" }}>
                  <Stack spacing={0.5} height="100%">
                      <Box
                        sx={{
                          display: "flex",
                          justifyContent: "space-between",
                          alignItems: "center",
                        }}
                      >
                        <Typography
                          variant="body2"
                          fontWeight={today ? 700 : 500}
                          sx={{ color: colors.text }}
                        >
                          {date.date()}
                        </Typography>
                        {daySummary && daySummary.totalHours > 0 && (
                          <Chip
                            label={formatDuration(daySummary.totalHours)}
                            size="small"
                            color="primary"
                            sx={{ height: 20, fontSize: "0.65rem" }}
                          />
                        )}
                      </Box>

                      {/* Show entry summaries */}
                      {daySummary && daySummary.entries.length > 0 && (
                        <Stack spacing={0.5} sx={{ flex: 1, overflow: "auto" }}>
                          {daySummary.entries.slice(0, 2).map((entry) => (
                            <Box
                              key={entry.aggregateRootId}
                              sx={{
                                p: 0.5,
                                borderRadius: 0.5,
                                backgroundColor: "background.paper",
                                border: 1,
                                borderColor: "divider",
                                fontSize: "0.65rem",
                              }}
                              onClick={(e) => {
                                e.stopPropagation();
                                onEdit(entry);
                              }}
                            >
                              <Typography
                                variant="caption"
                                sx={{
                                  display: "block",
                                  whiteSpace: "nowrap",
                                  overflow: "hidden",
                                  textOverflow: "ellipsis",
                                  fontSize: "0.65rem",
                                }}
                              >
                                {entry.project.code}
                              </Typography>
                              <Typography
                                variant="caption"
                                color="text.secondary"
                                sx={{ fontSize: "0.6rem" }}
                              >
                                {formatDuration(
                                  calculateDuration(entry.from, entry.until)
                                )}
                              </Typography>
                            </Box>
                          ))}
                          {daySummary.entries.length > 2 && (
                            <Typography
                              variant="caption"
                              color="text.secondary"
                              sx={{ fontSize: "0.6rem", textAlign: "center" }}
                            >
                              +{daySummary.entries.length - 2} more
                            </Typography>
                          )}
                        </Stack>
                      )}

                      {/* Add button */}
                      {!isFuture(date) && (
                        <Box sx={{ textAlign: "center", mt: "auto" }}>
                          <Tooltip title="Add time entry">
                            <IconButton
                              size="small"
                              onClick={(e) => {
                                e.stopPropagation();
                                onAddEntry(date);
                              }}
                              disabled={isLoading}
                            >
                              <Add fontSize="small" />
                            </IconButton>
                          </Tooltip>
                        </Box>
                      )}
                    </Stack>
                  </CardContent>
              </Card>
            </Grid>
          );
        })}
      </Grid>
    </Paper>
  );
};

export default MonthView;