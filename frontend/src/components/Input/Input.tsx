import { useState } from "react";
import './Input.css';

interface InputProps {
    setShortenedUrl: React.Dispatch<React.SetStateAction<string>>;
}

const URL_REGEX: RegExp = /^(https?:\/\/)?([\w-]+\.)+[\w-]{2,}(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$/;

const Input = ({ setShortenedUrl }: InputProps) => {
    const [originalUrl, setOriginalUrl] = useState<string>('');
    const [error, setError] = useState<string>('');

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setError('');
        setShortenedUrl('');
        setOriginalUrl(e.target.value)
    }

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!URL_REGEX.test(originalUrl)) {
            setError("Please, input a valid URL");
            return;
        }
        // TODO: change it to set api result
        setShortenedUrl(originalUrl);
    }

    return (
        <div className="input">
            <form onSubmit={e => handleSubmit(e)}>
                <div className="input-group">
                    <input
                        className="form-control"
                        type="text"
                        placeholder="Input your URL here"
                        onChange={handleChange}
                        id="url-value"
                        autoComplete="off"
                    />
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
