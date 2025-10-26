// Project domain interfaces based on EventStore API schema

export interface ReadProjectModel {
  aggregateRootId: string;
  name: string;
  code: string;
  billable: boolean;
  isRemoved: boolean;
  version: number;
}

export interface RegisterProjectRequest {
  name: string;
  code: string;
  billable: boolean;
}

export interface RegisterProjectResponse {
  id: string;
}

export interface UpdateProjectRequest {
  name: string;
  code: string;
  billable: boolean;
}

export interface UpdateProjectResponse {
  id: string;
}

export interface LoadAllProjectsResponse {
  projects: ReadProjectModel[];
}

export interface LoadSingleProjectResponse {
  project: ReadProjectModel;
}