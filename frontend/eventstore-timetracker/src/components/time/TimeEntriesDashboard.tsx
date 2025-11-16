import React, { useState, useEffect, useCallback } from "react";
import {
  Container,
  Paper,
  Typography,
  Box,
  Button,
  Alert,
  CircularProgress,
  ToggleButtonGroup,
  ToggleButton,
  IconButton,
  Stack,
} from "@mui/material";
import {
  Add,
  Refresh,
  CalendarViewWeek,
  CalendarMonth,
  ChevronLeft,
  ChevronRight,
} from "@mui/icons-material";
import dayjs, { type Dayjs } from "dayjs";
import { useNotification } from "../notification/hooks/use-notification";
import { useAuth } from "../../hooks/use-auth";
import type {
  ReadTimeEntryModel,
  RegisterTimeEntryRequest,
  UpdateTimeEntryRequest,
} from "../../interfaces/timeentries.interface";
import type { ReadProjectModel } from "../../interfaces/projects.interface";
import { TimeEntryService } from "../../services/timeentry.service";
import { ProjectService } from "../../services/project.service";
import TimeEntryFormDialog from "./TimeEntryFormDialog";
import TimeEntryDeleteDialog from "./TimeEntryDeleteDialog";
import WeekView from "./WeekView";
import MonthView from "./MonthView";
import DayDetailDialog from "./DayDetailDialog";
import { getWeekSummary, getMonthSummary } from "../../utilities/timeentry.utils";

type ViewMode = "week" | "month";

const TimeEntriesDashboard: React.FC = () => {
  const { showSuccess, showError } = useNotification();
  const { user } = useAuth();

  // State management
  const [timeEntries, setTimeEntries] = useState<ReadTimeEntryModel[]>([]);
  const [projects, setProjects] = useState<ReadProjectModel[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [loadError, setLoadError] = useState<string | null>(null);

  // View state
  const [viewMode, setViewMode] = useState<ViewMode>("week");
  const [currentDate, setCurrentDate] = useState<Dayjs>(dayjs());

  // Dialog state
  const [isFormDialogOpen, setIsFormDialogOpen] = useState(false);
  const [isDeleteDialogOpen, setIsDeleteDialogOpen] = useState(false);
  const [isDayDetailDialogOpen, setIsDayDetailDialogOpen] = useState(false);
  const [selectedEntry, setSelectedEntry] = useState<ReadTimeEntryModel | null>(null);
  const [selectedDate, setSelectedDate] = useState<Dayjs | null>(null);

  // Load data
  const loadData = useCallback(async () => {
    try {
      setIsLoading(true);
      setLoadError(null);

      const [entriesResponse, projectsResponse] = await Promise.all([
        TimeEntryService.getMyTimeEntries(),
        ProjectService.getAllProjects(),
      ]);

      setTimeEntries(entriesResponse.timeEntries || []);
      setProjects(projectsResponse.projects || []);
    } catch (error) {
      const errorMessage =
        error instanceof Error ? error.message : "Failed to load data";
      setLoadError(errorMessage);
      showError(errorMessage);
    } finally {
      setIsLoading(false);
    }
  }, [showError]);

  // Initial load
  useEffect(() => {
    loadData();
  }, [loadData]);

  // Navigation handlers
  const handlePrevious = () => {
    if (viewMode === "week") {
      setCurrentDate((prev) => prev.subtract(1, "week"));
    } else {
      setCurrentDate((prev) => prev.subtract(1, "month"));
    }
  };

  const handleNext = () => {
    if (viewMode === "week") {
      setCurrentDate((prev) => prev.add(1, "week"));
    } else {
      setCurrentDate((prev) => prev.add(1, "month"));
    }
  };

  const handleToday = () => {
    setCurrentDate(dayjs());
  };

  const handleViewModeChange = (
    _event: React.MouseEvent<HTMLElement>,
    newMode: ViewMode | null
  ) => {
    if (newMode) {
      setViewMode(newMode);
    }
  };

  // Entry management handlers
  const handleAddEntry = (date?: Dayjs) => {
    setSelectedEntry(null);
    setSelectedDate(date || null);
    setIsFormDialogOpen(true);
  };

  const handleEditEntry = (entry: ReadTimeEntryModel) => {
    setSelectedEntry(entry);
    setSelectedDate(null);
    setIsFormDialogOpen(true);
  };

  const handleDeleteClick = (entry: ReadTimeEntryModel) => {
    setSelectedEntry(entry);
    setIsDeleteDialogOpen(true);
  };

  const handleDayClick = (date: Dayjs) => {
    setSelectedDate(date);
    setIsDayDetailDialogOpen(true);
  };

  // Form submit handler
  const handleFormSubmit = async (
    data: RegisterTimeEntryRequest | UpdateTimeEntryRequest
  ) => {
    setIsSubmitting(true);
    try {
      if (selectedEntry) {
        // Update existing entry
        await TimeEntryService.updateTimeEntry(
          selectedEntry.aggregateRootId,
          data as UpdateTimeEntryRequest
        );
        showSuccess("Time entry updated successfully");
      } else {
        // Create new entry
        await TimeEntryService.registerTimeEntry(data as RegisterTimeEntryRequest);
        showSuccess("Time entry logged successfully");
      }
      await loadData();
      setIsFormDialogOpen(false);
    } catch (error) {
      const errorMessage =
        error instanceof Error
          ? error.message
          : selectedEntry
          ? "Failed to update time entry"
          : "Failed to log time entry";
      showError(errorMessage);
      throw error;
    } finally {
      setIsSubmitting(false);
    }
  };

  // Delete confirmation handler
  const handleDeleteConfirm = async () => {
    if (!selectedEntry) return;

    setIsSubmitting(true);
    try {
      await TimeEntryService.deleteTimeEntry(selectedEntry.aggregateRootId);
      showSuccess("Time entry deleted successfully");
      await loadData();
      setIsDeleteDialogOpen(false);
      setIsDayDetailDialogOpen(false);
    } catch (error) {
      const errorMessage =
        error instanceof Error ? error.message : "Failed to delete time entry";
      showError(errorMessage);
      throw error;
    } finally {
      setIsSubmitting(false);
    }
  };

  // Dialog close handlers
  const handleFormDialogClose = () => {
    setIsFormDialogOpen(false);
    setSelectedEntry(null);
    setSelectedDate(null);
  };

  const handleDeleteDialogClose = () => {
    setIsDeleteDialogOpen(false);
    setSelectedEntry(null);
  };

  const handleDayDetailDialogClose = () => {
    setIsDayDetailDialogOpen(false);
    setSelectedDate(null);
  };

  // Calculate summaries
  const weekSummary = getWeekSummary(timeEntries, currentDate);
  const monthSummary = getMonthSummary(timeEntries, currentDate);

  // Get entries for selected day
  const selectedDayEntries = selectedDate
    ? timeEntries.filter((entry) => {
        const entryDate = dayjs(entry.from);
        return entryDate.isSame(selectedDate, "day");
      })
    : [];

  // Get current period label
  const periodLabel =
    viewMode === "week"
      ? `Week ${weekSummary.weekNumber}, ${weekSummary.year}`
      : currentDate.format("MMMM YYYY");

  return (
    <Container maxWidth="xl" sx={{ py: 4, pb: 10 }}>
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
              Time Tracker
            </Typography>
            <Typography variant="body2" color="text.secondary">
              Manage your time entries across week and month views
            </Typography>
          </Box>

          <Stack direction="row" spacing={2} flexWrap="wrap">
            <Button
              variant="outlined"
              startIcon={<Refresh />}
              onClick={loadData}
              disabled={isLoading}
            >
              Refresh
            </Button>
            <Button
              variant="contained"
              startIcon={<Add />}
              onClick={() => handleAddEntry()}
              disabled={isLoading}
            >
              Log Time
            </Button>
          </Stack>
        </Box>
      </Paper>

      {/* Error Alert */}
      {loadError && (
        <Alert severity="error" sx={{ mb: 3 }} onClose={() => setLoadError(null)}>
          {loadError}
        </Alert>
      )}

      {/* Navigation and View Controls */}
      <Paper elevation={3} sx={{ p: 2, mb: 3 }}>
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            flexWrap: "wrap",
            gap: 2,
          }}
        >
          {/* Navigation */}
          <Stack direction="row" spacing={1} alignItems="center">
            <IconButton onClick={handlePrevious} disabled={isLoading}>
              <ChevronLeft />
            </IconButton>
            <Button onClick={handleToday} variant="outlined" disabled={isLoading}>
              Today
            </Button>
            <IconButton onClick={handleNext} disabled={isLoading}>
              <ChevronRight />
            </IconButton>
            <Typography variant="h6" sx={{ ml: 2, minWidth: 200 }}>
              {periodLabel}
            </Typography>
          </Stack>

          {/* View Mode Toggle */}
          <ToggleButtonGroup
            value={viewMode}
            exclusive
            onChange={handleViewModeChange}
            disabled={isLoading}
          >
            <ToggleButton value="week">
              <CalendarViewWeek sx={{ mr: 1 }} />
              Week
            </ToggleButton>
            <ToggleButton value="month">
              <CalendarMonth sx={{ mr: 1 }} />
              Month
            </ToggleButton>
          </ToggleButtonGroup>
        </Box>
      </Paper>

      {/* Loading State */}
      {isLoading && timeEntries.length === 0 ? (
        <Box sx={{ display: "flex", justifyContent: "center", py: 8 }}>
          <CircularProgress />
        </Box>
      ) : (
        /* View Content */
        <>
          {viewMode === "week" ? (
            <WeekView
              weekSummary={weekSummary}
              onEdit={handleEditEntry}
              onDelete={handleDeleteClick}
              onAddEntry={handleAddEntry}
              isLoading={isLoading}
            />
          ) : (
            <MonthView
              monthSummary={monthSummary}
              onEdit={handleEditEntry}
              onDelete={handleDeleteClick}
              onAddEntry={handleAddEntry}
              onDayClick={handleDayClick}
              isLoading={isLoading}
            />
          )}
        </>
      )}

      {/* Form Dialog (Create/Edit) */}
      <TimeEntryFormDialog
        open={isFormDialogOpen}
        timeEntry={selectedEntry}
        initialDate={selectedDate || undefined}
        projects={projects}
        currentUserId={user?.id || ""}
        onClose={handleFormDialogClose}
        onSubmit={handleFormSubmit}
        isLoading={isSubmitting}
      />

      {/* Delete Confirmation Dialog */}
      <TimeEntryDeleteDialog
        open={isDeleteDialogOpen}
        timeEntry={selectedEntry}
        onClose={handleDeleteDialogClose}
        onConfirm={handleDeleteConfirm}
        isLoading={isSubmitting}
      />

      {/* Day Detail Dialog */}
      {selectedDate && (
        <DayDetailDialog
          open={isDayDetailDialogOpen}
          date={selectedDate}
          entries={selectedDayEntries}
          onClose={handleDayDetailDialogClose}
          onAddEntry={() => {
            setIsDayDetailDialogOpen(false);
            handleAddEntry(selectedDate);
          }}
          onEdit={(entry) => {
            setIsDayDetailDialogOpen(false);
            handleEditEntry(entry);
          }}
          onDelete={handleDeleteClick}
          isLoading={isSubmitting}
        />
      )}
    </Container>
  );
};

export default TimeEntriesDashboard;