import re
from flask import Flask, request, jsonify
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
    response = supabase.table('games').select("*").execute()
    return response.data

@app.route('/scores')
def scores():
    game_id = request.args.get("game_id")
    today = datetime.now().strftime('%Y-%m-%d')
    response = supabase.from_('scores').select('player_id, score').gte('date', today + ' 00:00:00').lte('date', today + ' 23:59:59').eq("game_id", game_id).order('score', desc=True).execute()

    leaderboard = []
    for score in response.data:
        player = supabase.from_('players').select('name').eq('id', score['player_id']).execute()
        leaderboard.append({'player': player.data[0]['name'], 'score': score['score']})

    return leaderboard

@app.route('/scores', methods=['POST'])
def score():
    try:
        data = request.get_json()

        if not data or 'game_id' not in data or 'player_id' not in data or 'score' not in data:
            return jsonify({"error": "Dados inválidos"}), 400

        response = (
            supabase.table("scores")
            .insert(data)
            .execute()
        )

        return jsonify({"message": "Pontuação inserida com sucesso", "data": response.data}), 200

    except Exception as e:
        return jsonify({"error": e.__str__()}), 500

@app.route('/players')
def players():
    response = supabase.table('players').select("*").order('name').execute()
    return response.data

@app.route('/players', methods=['POST'])
def players_insert():
    try:
        data = request.get_json()

        if not data.get('name') or not isinstance(data['name'], str):
            return jsonify({"error": "Nome inválido"}), 400

        email_regex = r'^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$'
        if not data.get('email') or not re.match(email_regex, data['email']):
            return jsonify({"error": "Email inválido"}), 400

        if not data.get('interest_area') or not isinstance(data['interest_area'], str):
            return jsonify({"error": "Área de interesse inválida"}), 400

        if not data.get('graduation_year') or not isinstance(data['graduation_year'], int):
            return jsonify({"error": "Ano de graduação inválido"}), 400

        response = (
            supabase.table("players")
            .insert(data)
            .execute()
        )

        return jsonify({ "message": "Dados do jogador inseridos com sucesso", "data": response.data}), 200
    
    except Exception as e:
        return jsonify({"error": e.__str__()}), 500
    
if __name__ == "__main__":
    app.run('localhost', 5000)