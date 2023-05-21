import { ConditionsRecord } from "../models/conditionsRecord";

export function parseDateString(dateString: string): Date {
    return new Date(dateString);
}

export const unitByValueNameMap: Partial<Record<keyof ConditionsRecord, string>> = {
    temperature: "°C",
    humidity: "hum",
    light: "light",
    orientation: "(X, Y)",
};
