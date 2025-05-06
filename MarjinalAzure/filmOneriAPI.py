from fastapi import FastAPI, Request
from pydantic import BaseModel
from typing import List
from filmOneri import filmOneriBot

app = FastAPI()

class Preferences(BaseModel):
    userPreferences: List[str]

@app.post("/recommend")
def recommend_series(preferences: Preferences):
    return {"recommendations": filmOneriBot(preferences.userPreferences)}