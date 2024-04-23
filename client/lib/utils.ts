export async function sha256(input: string): Promise<string> {
    const encoder = new TextEncoder();
    const data = encoder.encode(input);
    return crypto.subtle.digest("SHA-256", data).then((hash) => {
        return Array.from(new Uint8Array(hash)).map((b) => b.toString(16).padStart(2, "0")).join("");
    });
}

export function parseDate(date: string | Date): Date {
    return typeof date == "string" ? new Date(date) : date as Date;
}