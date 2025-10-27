// Domain interface for Time Entry
export interface TimeEntry {
  id: string;
  userId: string;
  projectId: string;
  activityTypeId: string;
  date: string; // ISO date string
  durationInMinutes: number;
  comment: string;
  isBillable: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface CreateTimeEntryRequest {
  projectId: string;
  activityTypeId: string;
  date: string;
  durationInMinutes: number;
  comment: string;
  isBillable: boolean;
}

export interface UpdateTimeEntryRequest {
  id: string;
  projectId?: string;
  activityTypeId?: string;
  date?: string;
  durationInMinutes?: number;
  comment?: string;
  isBillable?: boolean;
}

export interface TimeEntryResponse {
  id: string;
  userId: string;
  projectId: string;
  projectName: string;
  activityTypeId: string;
  activityTypeName: string;
  date: string;
  durationInMinutes: number;
  comment: string;
  isBillable: boolean;
  createdAt: string;
  updatedAt: string;
}

export interface WeekTimeEntry {
  date: string;
  dayName: string;
  entries: TimeEntryResponse[];
  totalMinutes: number;
}