import axios from 'axios';

export interface GetShortenUrlRequest {
    originalUrl: string;
}

export interface GetShortenUrlResponse {
    shortenedUrl: string;
}

export const sendRequestData = async(
    request: GetShortenUrlRequest
): Promise<GetShortenUrlResponse> => {
    const baseAddress = process.env.REACT_APP_API_URL;
    const response = await axios.post<GetShortenUrlResponse>(
        `${baseAddress}/api/urls`,
        request
    );

    return response.data;
}
