import { useEffect, useState, useMemo } from "react";
import {
  Box,
  Card,
  CardContent,
  Typography,
  Grid,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Tabs,
  Tab,
  CircularProgress,
  Alert,
  Chip,
  LinearProgress,
  ToggleButton,
  ToggleButtonGroup,
} from "@mui/material";
import {
  TrendingUp,
  PieChart,
  BarChart,
  Timeline,
} from "@mui/icons-material";
import dayjs, { Dayjs } from "dayjs";
import { ReadTimeEntryModel } from "../../interfaces/timeentries.interface";
import { ReadProjectModel } from "../../interfaces/projects.interface";
import { TimeEntryService } from "../../services/timeentry.service";
import { ProjectService } from "../../services/project.service";
import { useNotification } from "../../components/notification/notification.context";
import { calculateDuration, formatDuration } from "../../utilities/timeentry.utils";
import { ActivityTypeApiValue } from "../../interfaces/timeentries.interface";

interface ActivityStats {
  activityType: ActivityTypeApiValue;
  totalHours: number;
  count: number;
  percentage: number;
}

interface ProjectStats {
  projectId: string;
  projectCode: string;
  totalHours: number;
  count: number;
  percentage: number;
  activities: Map<ActivityTypeApiValue, number>;
}

interface DailyStats {
  date: string;
  totalHours: number;
  count: number;
}

interface WeeklyStats {
  weekStart: string;
  totalHours: number;
  count: number;
}

type TimePeriod = "week" | "month" | "quarter" | "year" | "all";

const activityColors: Record<ActivityTypeApiValue, string> = {
  analysis: "#2196F3",
  development: "#4CAF50",
  testing: "#FF9800",
  codeReview: "#9C27B0",
  documentation: "#00BCD4",
  meeting: "#F44336",
  bugFix: "#FF5722",
  deployment: "#3F51B5",
  research: "#009688",
  other: "#9E9E9E",
};

const activityLabels: Record<ActivityTypeApiValue, string> = {
  analysis: "Analysis",
  development: "Development",
  testing: "Testing",
  codeReview: "Code Review",
  documentation: "Documentation",
  meeting: "Meeting",
  bugFix: "Bug Fix",
  deployment: "Deployment",
  research: "Research",
  other: "Other",
};

export function AnalyticsView() {
  const [timeEntries, setTimeEntries] = useState<ReadTimeEntryModel[]>([]);
  const [projects, setProjects] = useState<ReadProjectModel[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [loadError, setLoadError] = useState<string | null>(null);
  const [activeTab, setActiveTab] = useState(0);
  const [timePeriod, setTimePeriod] = useState<TimePeriod>("month");
  const { showError } = useNotification();

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      setIsLoading(true);
      setLoadError(null);

      const [entriesResponse, projectsResponse] = await Promise.all([
        TimeEntryService.getMyTimeEntries(),
        ProjectService.getAllProjects(),
      ]);

      setTimeEntries(entriesResponse.timeEntries);
      setProjects(projectsResponse.projects);
    } catch (error) {
      const message =
        error instanceof Error ? error.message : "Failed to load data";
      setLoadError(message);
      showError(message);
    } finally {
      setIsLoading(false);
    }
  };

  const filteredEntries = useMemo(() => {
    const now = dayjs();
    let cutoffDate: Dayjs;

    switch (timePeriod) {
      case "week":
        cutoffDate = now.subtract(7, "days");
        break;
      case "month":
        cutoffDate = now.subtract(1, "month");
        break;
      case "quarter":
        cutoffDate = now.subtract(3, "months");
        break;
      case "year":
        cutoffDate = now.subtract(1, "year");
        break;
      case "all":
      default:
        return timeEntries.filter((entry) => !entry.isRemoved);
    }

    return timeEntries.filter(
      (entry) =>
        !entry.isRemoved && dayjs(entry.from).isAfter(cutoffDate)
    );
  }, [timeEntries, timePeriod]);

  const activityStats = useMemo(() => {
    const stats = new Map<ActivityTypeApiValue, ActivityStats>();
    let totalHours = 0;

    filteredEntries.forEach((entry) => {
      const hours = calculateDuration(entry.from, entry.until);
      totalHours += hours;

      const existing = stats.get(entry.activityType);
      if (existing) {
        existing.totalHours += hours;
        existing.count += 1;
      } else {
        stats.set(entry.activityType, {
          activityType: entry.activityType,
          totalHours: hours,
          count: 1,
          percentage: 0,
        });
      }
    });

    // Calculate percentages
    stats.forEach((stat) => {
      stat.percentage = totalHours > 0 ? (stat.totalHours / totalHours) * 100 : 0;
    });

    return Array.from(stats.values()).sort((a, b) => b.totalHours - a.totalHours);
  }, [filteredEntries]);

  const projectStats = useMemo(() => {
    const stats = new Map<string, ProjectStats>();
    let totalHours = 0;

    filteredEntries.forEach((entry) => {
      const hours = calculateDuration(entry.from, entry.until);
      totalHours += hours;

      const existing = stats.get(entry.project.id);
      if (existing) {
        existing.totalHours += hours;
        existing.count += 1;
        const activityHours = existing.activities.get(entry.activityType) || 0;
        existing.activities.set(entry.activityType, activityHours + hours);
      } else {
        const activities = new Map<ActivityTypeApiValue, number>();
        activities.set(entry.activityType, hours);
        stats.set(entry.project.id, {
          projectId: entry.project.id,
          projectCode: entry.project.code,
          totalHours: hours,
          count: 1,
          percentage: 0,
          activities,
        });
      }
    });

    // Calculate percentages
    stats.forEach((stat) => {
      stat.percentage = totalHours > 0 ? (stat.totalHours / totalHours) * 100 : 0;
    });

    return Array.from(stats.values()).sort((a, b) => b.totalHours - a.totalHours);
  }, [filteredEntries]);

  const dailyStats = useMemo(() => {
    const stats = new Map<string, DailyStats>();

    filteredEntries.forEach((entry) => {
      const date = dayjs(entry.from).format("YYYY-MM-DD");
      const hours = calculateDuration(entry.from, entry.until);

      const existing = stats.get(date);
      if (existing) {
        existing.totalHours += hours;
        existing.count += 1;
      } else {
        stats.set(date, {
          date,
          totalHours: hours,
          count: 1,
        });
      }
    });

    return Array.from(stats.values()).sort((a, b) => a.date.localeCompare(b.date));
  }, [filteredEntries]);

  const weeklyStats = useMemo(() => {
    const stats = new Map<string, WeeklyStats>();

    filteredEntries.forEach((entry) => {
      const weekStart = dayjs(entry.from).startOf("isoWeek").format("YYYY-MM-DD");
      const hours = calculateDuration(entry.from, entry.until);

      const existing = stats.get(weekStart);
      if (existing) {
        existing.totalHours += hours;
        existing.count += 1;
      } else {
        stats.set(weekStart, {
          weekStart,
          totalHours: hours,
          count: 1,
        });
      }
    });

    return Array.from(stats.values()).sort((a, b) =>
      a.weekStart.localeCompare(b.weekStart)
    );
  }, [filteredEntries]);

  const totalStats = useMemo(() => {
    const totalHours = filteredEntries.reduce(
      (sum, entry) => sum + calculateDuration(entry.from, entry.until),
      0
    );
    const totalEntries = filteredEntries.length;
    const averagePerDay = dailyStats.length > 0 ? totalHours / dailyStats.length : 0;
    const averagePerEntry = totalEntries > 0 ? totalHours / totalEntries : 0;

    return {
      totalHours,
      totalEntries,
      averagePerDay,
      averagePerEntry,
      uniqueDays: dailyStats.length,
      uniqueProjects: projectStats.length,
    };
  }, [filteredEntries, dailyStats, projectStats]);

  if (isLoading) {
    return (
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          minHeight: "60vh",
        }}
      >
        <CircularProgress />
      </Box>
    );
  }

  if (loadError) {
    return (
      <Box sx={{ p: 3 }}>
        <Alert severity="error">{loadError}</Alert>
      </Box>
    );
  }

  return (
    <Box sx={{ p: 3 }}>
      <Box sx={{ mb: 3, display: "flex", justifyContent: "space-between", alignItems: "center" }}>
        <Typography variant="h4" component="h1" gutterBottom>
          Time Registration Analytics
        </Typography>
        <ToggleButtonGroup
          value={timePeriod}
          exclusive
          onChange={(_, value) => value && setTimePeriod(value)}
          size="small"
        >
          <ToggleButton value="week">Week</ToggleButton>
          <ToggleButton value="month">Month</ToggleButton>
          <ToggleButton value="quarter">Quarter</ToggleButton>
          <ToggleButton value="year">Year</ToggleButton>
          <ToggleButton value="all">All Time</ToggleButton>
        </ToggleButtonGroup>
      </Box>

      {/* Overview Cards */}
      <Grid container spacing={3} sx={{ mb: 3 }}>
        <Grid item xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Typography color="textSecondary" gutterBottom>
                Total Hours
              </Typography>
              <Typography variant="h4">
                {formatDuration(totalStats.totalHours)}
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Typography color="textSecondary" gutterBottom>
                Total Entries
              </Typography>
              <Typography variant="h4">{totalStats.totalEntries}</Typography>
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Typography color="textSecondary" gutterBottom>
                Avg Hours/Day
              </Typography>
              <Typography variant="h4">
                {formatDuration(totalStats.averagePerDay)}
              </Typography>
            </CardContent>
          </Card>
        </Grid>
        <Grid item xs={12} sm={6} md={3}>
          <Card>
            <CardContent>
              <Typography color="textSecondary" gutterBottom>
                Active Projects
              </Typography>
              <Typography variant="h4">{totalStats.uniqueProjects}</Typography>
            </CardContent>
          </Card>
        </Grid>
      </Grid>

      {/* Tabs */}
      <Paper sx={{ mb: 3 }}>
        <Tabs
          value={activeTab}
          onChange={(_, value) => setActiveTab(value)}
          variant="fullWidth"
        >
          <Tab icon={<PieChart />} label="By Activity" />
          <Tab icon={<BarChart />} label="By Project" />
          <Tab icon={<Timeline />} label="Time Trends" />
          <Tab icon={<TrendingUp />} label="Detailed Stats" />
        </Tabs>
      </Paper>

      {/* Tab Content */}
      {activeTab === 0 && (
        <Grid container spacing={3}>
          <Grid item xs={12} md={6}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Activity Distribution
                </Typography>
                {activityStats.map((stat) => (
                  <Box key={stat.activityType} sx={{ mb: 2 }}>
                    <Box sx={{ display: "flex", justifyContent: "space-between", mb: 1 }}>
                      <Box sx={{ display: "flex", alignItems: "center", gap: 1 }}>
                        <Box
                          sx={{
                            width: 12,
                            height: 12,
                            borderRadius: "50%",
                            bgcolor: activityColors[stat.activityType],
                          }}
                        />
                        <Typography variant="body2">
                          {activityLabels[stat.activityType]}
                        </Typography>
                      </Box>
                      <Typography variant="body2" color="textSecondary">
                        {formatDuration(stat.totalHours)} ({stat.percentage.toFixed(1)}%)
                      </Typography>
                    </Box>
                    <LinearProgress
                      variant="determinate"
                      value={stat.percentage}
                      sx={{
                        height: 8,
                        borderRadius: 1,
                        bgcolor: "grey.200",
                        "& .MuiLinearProgress-bar": {
                          bgcolor: activityColors[stat.activityType],
                        },
                      }}
                    />
                  </Box>
                ))}
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} md={6}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Activity Summary
                </Typography>
                <TableContainer>
                  <Table size="small">
                    <TableHead>
                      <TableRow>
                        <TableCell>Activity</TableCell>
                        <TableCell align="right">Entries</TableCell>
                        <TableCell align="right">Total Hours</TableCell>
                        <TableCell align="right">Avg/Entry</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {activityStats.map((stat) => (
                        <TableRow key={stat.activityType}>
                          <TableCell>
                            <Chip
                              label={activityLabels[stat.activityType]}
                              size="small"
                              sx={{
                                bgcolor: activityColors[stat.activityType],
                                color: "white",
                              }}
                            />
                          </TableCell>
                          <TableCell align="right">{stat.count}</TableCell>
                          <TableCell align="right">
                            {formatDuration(stat.totalHours)}
                          </TableCell>
                          <TableCell align="right">
                            {formatDuration(stat.totalHours / stat.count)}
                          </TableCell>
                        </TableRow>
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      )}

      {activeTab === 1 && (
        <Grid container spacing={3}>
          <Grid item xs={12} md={6}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Project Distribution
                </Typography>
                {projectStats.map((stat) => (
                  <Box key={stat.projectId} sx={{ mb: 2 }}>
                    <Box sx={{ display: "flex", justifyContent: "space-between", mb: 1 }}>
                      <Typography variant="body2" fontWeight="medium">
                        {stat.projectCode}
                      </Typography>
                      <Typography variant="body2" color="textSecondary">
                        {formatDuration(stat.totalHours)} ({stat.percentage.toFixed(1)}%)
                      </Typography>
                    </Box>
                    <LinearProgress
                      variant="determinate"
                      value={stat.percentage}
                      sx={{ height: 8, borderRadius: 1 }}
                    />
                  </Box>
                ))}
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} md={6}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Project Summary
                </Typography>
                <TableContainer>
                  <Table size="small">
                    <TableHead>
                      <TableRow>
                        <TableCell>Project</TableCell>
                        <TableCell align="right">Entries</TableCell>
                        <TableCell align="right">Total Hours</TableCell>
                        <TableCell align="right">Top Activity</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {projectStats.map((stat) => {
                        const topActivity = Array.from(stat.activities.entries()).sort(
                          (a, b) => b[1] - a[1]
                        )[0];
                        return (
                          <TableRow key={stat.projectId}>
                            <TableCell>{stat.projectCode}</TableCell>
                            <TableCell align="right">{stat.count}</TableCell>
                            <TableCell align="right">
                              {formatDuration(stat.totalHours)}
                            </TableCell>
                            <TableCell align="right">
                              {topActivity && (
                                <Chip
                                  label={activityLabels[topActivity[0]]}
                                  size="small"
                                  sx={{
                                    bgcolor: activityColors[topActivity[0]],
                                    color: "white",
                                  }}
                                />
                              )}
                            </TableCell>
                          </TableRow>
                        );
                      })}
                    </TableBody>
                  </Table>
                </TableContainer>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Project Activity Breakdown
                </Typography>
                <TableContainer>
                  <Table size="small">
                    <TableHead>
                      <TableRow>
                        <TableCell>Project</TableCell>
                        {Object.keys(activityLabels).map((activity) => (
                          <TableCell key={activity} align="right">
                            {activityLabels[activity as ActivityTypeApiValue]}
                          </TableCell>
                        ))}
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {projectStats.map((stat) => (
                        <TableRow key={stat.projectId}>
                          <TableCell>{stat.projectCode}</TableCell>
                          {Object.keys(activityLabels).map((activity) => {
                            const hours = stat.activities.get(
                              activity as ActivityTypeApiValue
                            ) || 0;
                            return (
                              <TableCell key={activity} align="right">
                                {hours > 0 ? formatDuration(hours) : "-"}
                              </TableCell>
                            );
                          })}
                        </TableRow>
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      )}

      {activeTab === 2 && (
        <Grid container spacing={3}>
          <Grid item xs={12} md={6}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Daily Time Trend
                </Typography>
                <Box sx={{ maxHeight: 400, overflow: "auto" }}>
                  <TableContainer>
                    <Table size="small">
                      <TableHead>
                        <TableRow>
                          <TableCell>Date</TableCell>
                          <TableCell align="right">Entries</TableCell>
                          <TableCell align="right">Hours</TableCell>
                          <TableCell>Distribution</TableCell>
                        </TableRow>
                      </TableHead>
                      <TableBody>
                        {dailyStats.slice(-30).reverse().map((stat) => {
                          const maxHours = Math.max(...dailyStats.map((s) => s.totalHours));
                          const percentage = (stat.totalHours / maxHours) * 100;
                          return (
                            <TableRow key={stat.date}>
                              <TableCell>
                                {dayjs(stat.date).format("MMM DD, YYYY")}
                              </TableCell>
                              <TableCell align="right">{stat.count}</TableCell>
                              <TableCell align="right">
                                {formatDuration(stat.totalHours)}
                              </TableCell>
                              <TableCell>
                                <LinearProgress
                                  variant="determinate"
                                  value={percentage}
                                  sx={{ height: 6, borderRadius: 1 }}
                                />
                              </TableCell>
                            </TableRow>
                          );
                        })}
                      </TableBody>
                    </Table>
                  </TableContainer>
                </Box>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} md={6}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Weekly Time Trend
                </Typography>
                <Box sx={{ maxHeight: 400, overflow: "auto" }}>
                  <TableContainer>
                    <Table size="small">
                      <TableHead>
                        <TableRow>
                          <TableCell>Week Starting</TableCell>
                          <TableCell align="right">Entries</TableCell>
                          <TableCell align="right">Hours</TableCell>
                          <TableCell>Distribution</TableCell>
                        </TableRow>
                      </TableHead>
                      <TableBody>
                        {weeklyStats.slice(-12).reverse().map((stat) => {
                          const maxHours = Math.max(...weeklyStats.map((s) => s.totalHours));
                          const percentage = (stat.totalHours / maxHours) * 100;
                          return (
                            <TableRow key={stat.weekStart}>
                              <TableCell>
                                {dayjs(stat.weekStart).format("MMM DD, YYYY")}
                              </TableCell>
                              <TableCell align="right">{stat.count}</TableCell>
                              <TableCell align="right">
                                {formatDuration(stat.totalHours)}
                              </TableCell>
                              <TableCell>
                                <LinearProgress
                                  variant="determinate"
                                  value={percentage}
                                  sx={{ height: 6, borderRadius: 1 }}
                                />
                              </TableCell>
                            </TableRow>
                          );
                        })}
                      </TableBody>
                    </Table>
                  </TableContainer>
                </Box>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      )}

      {activeTab === 3 && (
        <Grid container spacing={3}>
          <Grid item xs={12}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Detailed Statistics
                </Typography>
                <Grid container spacing={2}>
                  <Grid item xs={12} sm={6} md={4}>
                    <Paper sx={{ p: 2, bgcolor: "primary.light", color: "primary.contrastText" }}>
                      <Typography variant="body2">Total Time Tracked</Typography>
                      <Typography variant="h5">
                        {formatDuration(totalStats.totalHours)}
                      </Typography>
                    </Paper>
                  </Grid>
                  <Grid item xs={12} sm={6} md={4}>
                    <Paper sx={{ p: 2, bgcolor: "secondary.light", color: "secondary.contrastText" }}>
                      <Typography variant="body2">Total Entries</Typography>
                      <Typography variant="h5">{totalStats.totalEntries}</Typography>
                    </Paper>
                  </Grid>
                  <Grid item xs={12} sm={6} md={4}>
                    <Paper sx={{ p: 2, bgcolor: "success.light", color: "success.contrastText" }}>
                      <Typography variant="body2">Active Days</Typography>
                      <Typography variant="h5">{totalStats.uniqueDays}</Typography>
                    </Paper>
                  </Grid>
                  <Grid item xs={12} sm={6} md={4}>
                    <Paper sx={{ p: 2, bgcolor: "info.light", color: "info.contrastText" }}>
                      <Typography variant="body2">Average Hours per Day</Typography>
                      <Typography variant="h5">
                        {formatDuration(totalStats.averagePerDay)}
                      </Typography>
                    </Paper>
                  </Grid>
                  <Grid item xs={12} sm={6} md={4}>
                    <Paper sx={{ p: 2, bgcolor: "warning.light", color: "warning.contrastText" }}>
                      <Typography variant="body2">Average per Entry</Typography>
                      <Typography variant="h5">
                        {formatDuration(totalStats.averagePerEntry)}
                      </Typography>
                    </Paper>
                  </Grid>
                  <Grid item xs={12} sm={6} md={4}>
                    <Paper sx={{ p: 2, bgcolor: "error.light", color: "error.contrastText" }}>
                      <Typography variant="body2">Active Projects</Typography>
                      <Typography variant="h5">{totalStats.uniqueProjects}</Typography>
                    </Paper>
                  </Grid>
                </Grid>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} md={6}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Most Productive Days
                </Typography>
                <TableContainer>
                  <Table size="small">
                    <TableHead>
                      <TableRow>
                        <TableCell>Date</TableCell>
                        <TableCell align="right">Hours</TableCell>
                        <TableCell align="right">Entries</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {dailyStats
                        .sort((a, b) => b.totalHours - a.totalHours)
                        .slice(0, 10)
                        .map((stat) => (
                          <TableRow key={stat.date}>
                            <TableCell>
                              {dayjs(stat.date).format("MMM DD, YYYY (ddd)")}
                            </TableCell>
                            <TableCell align="right">
                              {formatDuration(stat.totalHours)}
                            </TableCell>
                            <TableCell align="right">{stat.count}</TableCell>
                          </TableRow>
                        ))}
                    </TableBody>
                  </Table>
                </TableContainer>
              </CardContent>
            </Card>
          </Grid>
          <Grid item xs={12} md={6}>
            <Card>
              <CardContent>
                <Typography variant="h6" gutterBottom>
                  Activity Type Rankings
                </Typography>
                <TableContainer>
                  <Table size="small">
                    <TableHead>
                      <TableRow>
                        <TableCell>Rank</TableCell>
                        <TableCell>Activity</TableCell>
                        <TableCell align="right">Hours</TableCell>
                        <TableCell align="right">Percentage</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {activityStats.map((stat, index) => (
                        <TableRow key={stat.activityType}>
                          <TableCell>#{index + 1}</TableCell>
                          <TableCell>
                            <Chip
                              label={activityLabels[stat.activityType]}
                              size="small"
                              sx={{
                                bgcolor: activityColors[stat.activityType],
                                color: "white",
                              }}
                            />
                          </TableCell>
                          <TableCell align="right">
                            {formatDuration(stat.totalHours)}
                          </TableCell>
                          <TableCell align="right">
                            {stat.percentage.toFixed(1)}%
                          </TableCell>
                        </TableRow>
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>
              </CardContent>
            </Card>
          </Grid>
        </Grid>
      )}
    </Box>
  );
}
