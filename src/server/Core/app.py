from flask import Flask
import os
from supabase import create_client, Client
from dotenv import load_dotenv
from datetime import datetime

load_dotenv()

url: str = os.environ.get("SUPABASE_URL")
key: str = os.environ.get("SUPABASE_KEY")
supabase: Client = create_client(url, key)

app = Flask(__name__)

@app.route('/')
def home():
    return "Hello, Flask!"

@app.route('/games')
def games():
    response = supabase.table('game').select("*").execute()
    return response.data

@app.route('/leaderboard')
def leaderboard():

    today = datetime.now().strftime('%Y-%m-%d')
    response = supabase.from_('score').select('player_id, value').gte('date', today + ' 00:00:00').lte('date', today + ' 23:59:59').order('value', desc=True).execute()

    leaderboard = []
    for score in response.data:
        player = supabase.from_('player').select('name').eq('id', score['player_id']).execute()
        leaderboard.append({'player': player.data[0]['name'], 'score': score['value']})

    return leaderboard

@app.route('/players')
def players():
    response = supabase.table('player').select("*").order('name').execute()
    return response.data

if __name__ == "__main__":
    app.run(debug=True)
