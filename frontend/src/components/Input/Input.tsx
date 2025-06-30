import {
    useEffect,
    useState,
    useRef } from "react";
import './Input.css';
import useFetch from "../../hooks/useFetch";
import {
    GetShortenUrlRequest,
    GetShortenUrlResponse,
    sendRequestData
} from "../../models/shortenUrlModels";

interface InputProps {
    setShortenedUrl: React.Dispatch<React.SetStateAction<string>>;
    setIsLoading: React.Dispatch<React.SetStateAction<boolean>>;
}

const URL_REGEX: RegExp = /^(https?:\/\/)?([\w-]+\.)+[\w-]{2,}(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$/;

const Input = ({ setShortenedUrl, setIsLoading }: InputProps) => {
    const [originalUrl, setOriginalUrl] = useState<string>('');
    const [error, setError] = useState<string>('');
    const inputRef = useRef<HTMLInputElement>(null);

    const { execute, loading, fetchError, data } = useFetch<GetShortenUrlRequest, GetShortenUrlResponse>(sendRequestData);

    useEffect(() => {
        if (fetchError !== null)
            setError('An error has happened. Try again!');
    }, [fetchError])

    useEffect(() => {
        setIsLoading(loading);
    }, [loading, setIsLoading])

    useEffect(() => {
        if (data !== null)
            setShortenedUrl(data.shortenedUrl);
    }, [data, setShortenedUrl])

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setInitialState();
        setOriginalUrl(e.target.value);
    }

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setInitialState();
        if (!URL_REGEX.test(originalUrl)) {
            setError("Please, input a valid URL");
            setIsLoading(false);
            return;
        }
        try {
            const request: GetShortenUrlRequest = {
                originalUrl
            };
            await execute(request);
        } catch (error) {
            console.error("Error:", error);
        }
    }

    const setInitialState = () => {
        setError('');
        setShortenedUrl('');
    }

    return (
        <div className="input">
            <form onSubmit={e => handleSubmit(e)}>
                <div className="input-group position-relative">
                    <input
                        className="form-control"
                        type="text"
                        placeholder="Input your URL here"
                        onChange={handleChange}
                        id="url-value"
                        autoComplete="off"
                        value={originalUrl}
                        ref={inputRef}
                    />
                    <button
                        type="button"
                        className="btn btn-clear btn-sm position-absolute top-50 translate-middle-y"
                        style={{ zIndex: 100 }}
                        onClick={() => { setOriginalUrl(''); setInitialState(); inputRef.current?.focus(); }}
                    >
                        Clear
                    </button>
                    <button
                        className="btn btn-info"
                        disabled={originalUrl === ""}
                        type="submit">
                        Generate
                    </button>
                </div>
            </form>
            <p className="text-danger">{error}</p>
        </div>
    )
}

export default Input;
