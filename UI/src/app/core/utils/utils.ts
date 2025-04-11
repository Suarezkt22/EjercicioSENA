export function createApiClient<T extends Record<string, string>>(baseUrl: string, endpoints: T): Record<keyof T, string> {
    return Object.fromEntries(
      Object.entries(endpoints).map(([key, endpoint]) => [key, `${baseUrl}${endpoint}`])
    ) as Record<keyof T, string>;
  }
  