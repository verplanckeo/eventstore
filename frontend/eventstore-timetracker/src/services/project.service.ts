import type { 
    LoadAllProjectsResponse, 
    LoadSingleProjectResponse, 
    RegisterProjectRequest, 
    RegisterProjectResponse, 
    UpdateProjectRequest, 
    UpdateProjectResponse 
} from "../interfaces/projects.interface";

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

export class ProjectService {
  /**
   * Get all projects
   */
  static async getAllProjects(): Promise<LoadAllProjectsResponse> {
    const response = await fetch(`${API_BASE_URL}/Projects`, {
      method: "GET",
      headers: createHeaders(),
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch projects: ${response.statusText}`);
    }

    return response.json();
  }

  /**
   * Get a single project by ID
   */
  static async getProjectById(id: string): Promise<LoadSingleProjectResponse> {
    const response = await fetch(`${API_BASE_URL}/Projects/${id}`, {
      method: "GET",
      headers: createHeaders(),
    });

    if (!response.ok) {
      throw new Error(`Failed to fetch project: ${response.statusText}`);
    }

    return response.json();
  }

  /**
   * Register a new project
   */
  static async registerProject(
    request: RegisterProjectRequest
  ): Promise<RegisterProjectResponse> {
    const response = await fetch(`${API_BASE_URL}/Projects`, {
      method: "POST",
      headers: createHeaders(),
      body: JSON.stringify(request),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || `Failed to register project: ${response.statusText}`);
    }

    return response.json();
  }

  /**
   * Update an existing project
   */
  static async updateProject(
    id: string,
    request: UpdateProjectRequest
  ): Promise<UpdateProjectResponse> {
    const response = await fetch(`${API_BASE_URL}/Projects/${id}`, {
      method: "PUT",
      headers: createHeaders(),
      body: JSON.stringify(request),
    });

    if (!response.ok) {
      const errorText = await response.text();
      throw new Error(errorText || `Failed to update project: ${response.statusText}`);
    }

    return response.json();
  }

  /**
   * Delete a project
   */
  static async deleteProject(id: string): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Projects/${id}`, {
      method: "DELETE",
      headers: createHeaders(),
    });

    if (!response.ok) {
      throw new Error(`Failed to delete project: ${response.statusText}`);
    }
  }
}