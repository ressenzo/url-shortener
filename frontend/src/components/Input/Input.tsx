import { useState } from "react";
import './Input.css';

const Input = () => {
    const [url, setUrl] = useState<string>('');

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
    }

    return (
        <div className="input">
            <form onSubmit={e => handleSubmit(e)}>
                <div className="input-group">
                    <input
                        className="form-control"
                        type="text"
                        placeholder="Input your URL here"
                        onChange={e => setUrl(e.target.value)}
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
        </div>
    )
}

export default Input;
