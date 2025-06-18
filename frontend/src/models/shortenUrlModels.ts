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
    const response = await axios.post<GetShortenUrlResponse>(
        'http://localhost:5147/api/urls',
        request
    );

    return response.data;
}
