import dayjs, { type Dayjs } from "dayjs";
import isoWeek from "dayjs/plugin/isoWeek";
import weekOfYear from "dayjs/plugin/weekOfYear";
import type {
  ReadTimeEntryModel,
  TimeEntrySummary,
  WeekSummary,
  MonthSummary,
} from "../interfaces/timeentries.interface";

// Extend dayjs with plugins
dayjs.extend(isoWeek);
dayjs.extend(weekOfYear);

/**
 * Calculate duration in hours between two date-time strings
 */
export const calculateDuration = (from: string, until: string): number => {
  const fromDate = dayjs(from);
  const untilDate = dayjs(until);
  const diffInMinutes = untilDate.diff(fromDate, "minute");
  return Math.round((diffInMinutes / 60) * 100) / 100; // Round to 2 decimal places
};

/**
 * Format duration as hours and minutes
 */
export const formatDuration = (hours: number): string => {
  const h = Math.floor(hours);
  const m = Math.round((hours - h) * 60);
  
  if (m === 0) {
    return `${h}h`;
  }
  return `${h}h ${m}m`;
};

/**
 * Get date in YYYY-MM-DD format
 */
export const formatDate = (date: Dayjs | string): string => {
  return dayjs(date).format("YYYY-MM-DD");
};

/**
 * Group time entries by date
 */
export const groupEntriesByDate = (
  entries: ReadTimeEntryModel[]
): Map<string, ReadTimeEntryModel[]> => {
  const grouped = new Map<string, ReadTimeEntryModel[]>();

  entries.forEach((entry) => {
    const date = formatDate(entry.from);
    const existing = grouped.get(date) || [];
    grouped.set(date, [...existing, entry]);
  });

  return grouped;
};

/**
 * Calculate total hours for a set of entries
 */
export const calculateTotalHours = (entries: ReadTimeEntryModel[]): number => {
  return entries.reduce((total, entry) => {
    return total + calculateDuration(entry.from, entry.until);
  }, 0);
};

/**
 * Create daily summaries from time entries
 */
export const createDailySummaries = (
  entries: ReadTimeEntryModel[],
  startDate: Dayjs,
  endDate: Dayjs
): TimeEntrySummary[] => {
  const grouped = groupEntriesByDate(entries);
  const summaries: TimeEntrySummary[] = [];

  let currentDate = startDate;
  while (currentDate.isBefore(endDate) || currentDate.isSame(endDate, "day")) {
    const dateStr = formatDate(currentDate);
    const dayEntries = grouped.get(dateStr) || [];
    
    summaries.push({
      date: dateStr,
      totalHours: calculateTotalHours(dayEntries),
      entries: dayEntries,
    });

    currentDate = currentDate.add(1, "day");
  }

  return summaries;
};

/**
 * Get week summary for a specific week
 */
export const getWeekSummary = (
  entries: ReadTimeEntryModel[],
  date: Dayjs
): WeekSummary => {
  const startOfWeek = date.startOf("isoWeek");
  const endOfWeek = date.endOf("isoWeek");

  const weekEntries = entries.filter((entry) => {
    const entryDate = dayjs(entry.from);
    return (
      (entryDate.isAfter(startOfWeek) || entryDate.isSame(startOfWeek, "day")) &&
      (entryDate.isBefore(endOfWeek) || entryDate.isSame(endOfWeek, "day"))
    );
  });

  const dailySummaries = createDailySummaries(weekEntries, startOfWeek, endOfWeek);

  return {
    weekNumber: date.isoWeek(),
    year: date.year(),
    startDate: formatDate(startOfWeek),
    endDate: formatDate(endOfWeek),
    totalHours: calculateTotalHours(weekEntries),
    dailySummaries,
  };
};

/**
 * Get month summary for a specific month
 */
export const getMonthSummary = (
  entries: ReadTimeEntryModel[],
  date: Dayjs
): MonthSummary => {
  const startOfMonth = date.startOf("month");
  const endOfMonth = date.endOf("month");

  const monthEntries = entries.filter((entry) => {
    const entryDate = dayjs(entry.from);
    return (
      (entryDate.isAfter(startOfMonth) || entryDate.isSame(startOfMonth, "day")) &&
      (entryDate.isBefore(endOfMonth) || entryDate.isSame(endOfMonth, "day"))
    );
  });

  const dailySummaries = createDailySummaries(monthEntries, startOfMonth, endOfMonth);

  return {
    month: date.month() + 1, // dayjs months are 0-indexed
    year: date.year(),
    totalHours: calculateTotalHours(monthEntries),
    dailySummaries,
  };
};

/**
 * Get current week start and end dates
 */
export const getCurrentWeek = (): { start: Dayjs; end: Dayjs } => {
  const now = dayjs();
  return {
    start: now.startOf("isoWeek"),
    end: now.endOf("isoWeek"),
  };
};

/**
 * Get current month start and end dates
 */
export const getCurrentMonth = (): { start: Dayjs; end: Dayjs } => {
  const now = dayjs();
  return {
    start: now.startOf("month"),
    end: now.endOf("month"),
  };
};

/**
 * Get days in month as array
 */
export const getDaysInMonth = (date: Dayjs): Dayjs[] => {
  const daysInMonth = date.daysInMonth();
  const days: Dayjs[] = [];

  for (let i = 1; i <= daysInMonth; i++) {
    days.push(date.date(i));
  }

  return days;
};

/**
 * Get weekdays (Monday to Sunday)
 */
export const getWeekdays = (): string[] => {
  return ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"];
};

/**
 * Check if a date is today
 */
export const isToday = (date: Dayjs | string): boolean => {
  return dayjs(date).isSame(dayjs(), "day");
};

/**
 * Check if a date is in the past
 */
export const isPast = (date: Dayjs | string): boolean => {
  return dayjs(date).isBefore(dayjs(), "day");
};

/**
 * Check if a date is in the future
 */
export const isFuture = (date: Dayjs | string): boolean => {
  return dayjs(date).isAfter(dayjs(), "day");
};

/**
 * Validate time entry duration
 */
export const validateDuration = (from: string, until: string): string | null => {
  const fromDate = dayjs(from);
  const untilDate = dayjs(until);

  if (!fromDate.isValid()) {
    return "Invalid start date/time";
  }

  if (!untilDate.isValid()) {
    return "Invalid end date/time";
  }

  if (untilDate.isBefore(fromDate)) {
    return "End time must be after start time";
  }

  const duration = calculateDuration(from, until);
  if (duration > 24) {
    return "Duration cannot exceed 24 hours";
  }

  if (duration === 0) {
    return "Duration must be greater than 0";
  }

  return null;
};