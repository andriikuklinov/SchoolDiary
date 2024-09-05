export const serverUrl = 'http://localhost:5203/api/';

export interface Response<T> {
    isSuccess: boolean,
    result: T,
    errorMessage: string
} 