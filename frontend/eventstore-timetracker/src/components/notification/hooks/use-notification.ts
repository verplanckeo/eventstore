import { useContext } from "react";
import { NotificationContext } from "../notification.context";
import type { NotificationContextType } from "../notification.provider";


export const useNotification = (): NotificationContextType => {
    const context = useContext(NotificationContext);
    if (!context) {
        throw new Error(
            "useNotification must be used within a NotificationProvider"
        );
    }

    return context;
};