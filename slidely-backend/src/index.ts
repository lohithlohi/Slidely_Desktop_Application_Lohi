import express, { Request, Response } from 'express';
import fs from 'fs';
import path from 'path';

const app = express();
const port = 3000;

app.use(express.json());

// Define the path to the database file
const dbPath = path.join(__dirname, 'db.json');

// Initialize the database file if it doesn't exist
if (!fs.existsSync(dbPath)) {
    fs.writeFileSync(dbPath, JSON.stringify({ submissions: [] }, null, 2));
}

// Helper function to read the database
const readDatabase = () => {
    const data = fs.readFileSync(dbPath, 'utf-8');
    return JSON.parse(data);
};

// Helper function to write to the database
const writeDatabase = (data: any) => {
    fs.writeFileSync(dbPath, JSON.stringify(data, null, 2));
};

// Endpoint to check server status
app.get('/ping', (req: Request, res: Response) => {
    res.json({ success: true });
});

// Endpoint to submit form data
app.post('/submit', (req: Request, res: Response) => {
    try {
        const { name, email, phone, githublink, stopwatchtime } = req.body;

        if (!name || !email || !phone || !githublink || !stopwatchtime) {
            return res.status(400).json({ error: 'All fields are required' });
        }

        const db = readDatabase();
        const submissions = db.submissions;

        submissions.push({ name, email, phone, githublink, stopwatchtime });
        writeDatabase(db);

        res.status(201).json({ success: true });
    } catch (error) {
        res.status(500).json({ error: 'Internal Server Error' });
    }
});


// Endpoint to read form data by index
app.get('/read', (req: Request, res: Response) => {
    const index = parseInt(req.query.index as string);
    const db = readDatabase();
    const submissions = db.submissions;

    if (index >= 0 && index < submissions.length) {
        res.json(submissions[index]);
    } else {
        res.status(404).json({ error: 'Submission not found' });
    }
});

// Endpoint to read all submissions
app.get('/read_all', (req: Request, res: Response) => {
    const db = readDatabase();
    const submissions = db.submissions;

    if (submissions.length === 0) {
        res.status(200).json({ message: 'No submissions found.' });
    } else {
        res.json(submissions);
    }
});

// Endpoint to delete a submission by index
app.delete('/delete', (req: Request, res: Response) => {
    const index = parseInt(req.query.index as string);
    const db = readDatabase();
    const submissions = db.submissions;

    if (index >= 0 && index < submissions.length) {
        submissions.splice(index, 1);
        writeDatabase(db);
        res.json({ success: true });
    } else {
        res.status(404).json({ error: 'Submission not found' });
    }
});

// Endpoint to update a submission by index
app.put('/update', (req: Request, res: Response) => {
    const index = parseInt(req.query.index as string);
    const { name, email, phone, githublink, stopwatchtime } = req.body;
    const db = readDatabase();
    const submissions = db.submissions;

    if (index >= 0 && index < submissions.length) {
        submissions[index] = { name, email, phone, githublink, stopwatchtime };
        writeDatabase(db);
        res.json({ success: true });
    } else {
        res.status(404).json({ error: 'Submission not found' });
    }
});

// Root route
app.get('/', (req: Request, res: Response) => {
    res.send('Welcome to the Slidely Backend Server');
});

// Start the server
app.listen(port, () => {
    console.log(`Server is running on http://localhost:${port}`);
});
