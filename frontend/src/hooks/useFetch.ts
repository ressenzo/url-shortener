import { useState } from "react";

export const useFetch = <TParams = any, TResponse = any>(
    apiFunction: (params: TParams) => Promise<TResponse>
) => {
    const [loading, setLoading] = useState<boolean>(false);
    const [fetchError, setFetchError] = useState<Error | null>(null);
    const [data, setData] = useState<TResponse | null>(null);

    const execute = async (params: TParams): Promise<TResponse> => {
        setLoading(true);
        setFetchError(null);
        try {
            const response = await apiFunction(params);
            setData(response);
            return response;
        } catch (err) {
            const errorObj = err as Error;
            setFetchError(errorObj);
            throw errorObj;
        } finally {
            setLoading(false);
        }
    }

    return { execute, loading, fetchError, data };
}

export default useFetch;