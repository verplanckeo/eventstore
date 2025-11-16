import type {
  LoadAllTimeEntriesResponse,
  LoadSingleTimeEntryResponse,
  RegisterTimeEntryRequest,
  RegisterTimeEntryResponse,
  UpdateTimeEntryRequest,
  UpdateTimeEntryResponse,
} from "../interfaces/timeentries.interface";

const API_BASE_URL = "http://localhost:4000/api";

// Helper to get auth token
const getAuthToken = (): string | null => {
  return localStorage.getItem("auth_token");
};

// Helper to create headers with auth
const createHeaders = (): HeadersInit => {
  const token = getAuthToken();
  const headers: HeadersInit = {
    "Content-Type": "application/json",
  };

  if (token) {
    headers.Authorization = `Bearer ${token}`;
  }

  return headers;
};

export class TimeEntryService {
  /**
   * Get all time entries
   */
  static async getAllTimeEntries(): Promise<LoadAllTimeEntriesResponse> {
    const response = await fetch(`${API_BASE_URL}/TimeEntries`, {
      method: "GET",
      headers: createHeaders(),
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch time entries: ${response.statusText}`);
    }

    return response.json();
  }

  /**
   * Get current user's time entries
   */
  static async getMyTimeEntries(): Promise<LoadAllTimeEntriesResponse> {
    const response = await fetch(`${API_BASE_URL}/TimeEntries/me`, {
      method: "GET",
      headers: createHeaders(),
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch my time entries: ${response.statusText}`);
    }

    return response.json();
  }

  /**
   * Get a single time entry by ID
   */
  static async getTimeEntryById(id: string): Promise<LoadSingleTimeEntryResponse> {
    const response = await fetch(`${API_BASE_URL}/TimeEntries/${id}`, {
      method: "GET",
      headers: createHeaders(),
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch time entry: ${response.statusText}`);
    }

    return response.json();
  }

  /**
   * Register a new time entry
   */
  static async registerTimeEntry(
    request: RegisterTimeEntryRequest
  ): Promise<RegisterTimeEntryResponse> {
    const response = await fetch(`${API_BASE_URL}/TimeEntries`, {
      method: "POST",
      headers: createHeaders(),
      body: JSON.stringify(request),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || `Failed to register time entry: ${response.statusText}`);
    }

    return response.json();
  }

  /**
   * Update an existing time entry
   */
  static async updateTimeEntry(
    id: string,
    request: UpdateTimeEntryRequest
  ): Promise<UpdateTimeEntryResponse> {
    const response = await fetch(`${API_BASE_URL}/TimeEntries/${id}`, {
      method: "PUT",
      headers: createHeaders(),
      body: JSON.stringify(request),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || `Failed to update time entry: ${response.statusText}`);
    }

    return response.json();
  }

  /**
   * Delete a time entry
   */
  static async deleteTimeEntry(id: string): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/TimeEntries/${id}`, {
      method: "DELETE",
      headers: createHeaders(),
    });

    if (!response.ok) {
      throw new Error(`Failed to delete time entry: ${response.statusText}`);
    }
  }
}