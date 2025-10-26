import { createContext } from "react";
import type { NotificationContextType } from "./notification.provider";

export const NotificationContext = createContext<
	NotificationContextType | undefined
>(undefined);
