import React, { useState, useEffect, type ReactNode, useCallback } from "react";
import { AuthContext } from "./auth.context";
import type { AuthUser, AuthenticateRequest, AuthenticateResponse } from "../interfaces/authentication.interface";

interface AuthProviderProps {
  children: ReactNode;
}

const TOKEN_KEY = "auth_token";
const USER_KEY = "auth_user";

// Helper function to decode JWT token (basic decoding without validation)
const decodeToken = (token: string): AuthUser | null => {
  try {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
        .join("")
    );

    const payload = JSON.parse(jsonPayload);
    
    return {
      id: payload.sub,
      userName: payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"],
    };
  } catch (error) {
    console.error("Failed to decode token:", error);
    return null;
  }
};

// Helper function to check if token is expired
const isTokenExpired = (token: string): boolean => {
  try {
    const base64Url = token.split(".")[1];
    const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
        .join("")
    );

    const payload = JSON.parse(jsonPayload);
    const exp = payload.exp;
    
    if (!exp) return true;
    
    // Check if token is expired (with 10 second buffer)
    return Date.now() >= exp * 1000 - 10000;
  } catch (error) {
    return true;
  }
};

export const AuthProvider: React.FC<AuthProviderProps> = ({ children }) => {
  const [isLoading, setIsLoading] = useState(true);
  const [token, setToken] = useState<string | null>(null);
  const [user, setUser] = useState<AuthUser | null>(null);

  // Initialize auth state from localStorage
  useEffect(() => {
    const initializeAuth = () => {
      try {
        const storedToken = localStorage.getItem(TOKEN_KEY);
        const storedUser = localStorage.getItem(USER_KEY);

        if (storedToken && storedUser) {
          // Check if token is expired
          if (isTokenExpired(storedToken)) {
            // Token expired, clear storage
            localStorage.removeItem(TOKEN_KEY);
            localStorage.removeItem(USER_KEY);
          } else {
            setToken(storedToken);
            setUser(JSON.parse(storedUser));
          }
        }
      } catch (error) {
        console.error("Failed to initialize auth:", error);
      } finally {
        setIsLoading(false);
      }
    };

    initializeAuth();
  }, []);

  const login = useCallback(async (userName: string, password: string): Promise<void> => {
    try {
      const request: AuthenticateRequest = { userName, password };

      const response = await fetch("http://localhost:4000/api/Users/authenticate", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(request),
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(errorText || `Authentication failed: ${response.statusText}`);
      }

      const data: AuthenticateResponse = await response.json();

      // Decode token to get user info
      const decodedUser = decodeToken(data.token);
      
      if (!decodedUser) {
        throw new Error("Failed to decode authentication token");
      }

      // Store in state
      setToken(data.token);
      setUser(decodedUser);

      // Persist to localStorage
      localStorage.setItem(TOKEN_KEY, data.token);
      localStorage.setItem(USER_KEY, JSON.stringify(decodedUser));
    } catch (error) {
      // Clear any existing auth state on error
      setToken(null);
      setUser(null);
      localStorage.removeItem(TOKEN_KEY);
      localStorage.removeItem(USER_KEY);
      throw error;
    }
  }, []);

  const logout = useCallback(() => {
    setToken(null);
    setUser(null);
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
  }, []);

  const value = {
    isAuthenticated: !!token && !!user,
    isLoading,
    user,
    token,
    login,
    logout,
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};