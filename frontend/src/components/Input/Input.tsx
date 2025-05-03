import { useState } from "react";
import './Input.css';

interface InputProps {
    setResult: React.Dispatch<React.SetStateAction<string>>;
}

const URL_REGEX: RegExp = /^(https?:\/\/)?([\w\-]+\.)+[\w\-]{2,}(\/[\w\-._~:/?#[\]@!$&'()*+,;=]*)?$/;

const Input = ({ setResult }: InputProps) => {
    const [url, setUrl] = useState<string>('');
    const [error, setError] = useState<string>('');

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setError('');
        setResult('');
        setUrl(e.target.value)
    }

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!URL_REGEX.test(url)) {
            setError("Please, input a valid URL");
            return;
        }
        setResult(url);
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
                        disabled={url === ""}
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
