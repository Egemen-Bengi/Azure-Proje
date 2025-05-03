import { useState } from 'react'
import axios from 'axios'

function MoviesRecommender() {
  const [recommendations, setRecommendations] = useState([])
  const [preferences, setPreferences] = useState(["romantik", "komedi"])

  const getRecommendations = async () => {
    const response = await axios.post('http://localhost:8000/recommend', {
      userPreferences: preferences
    })
    setRecommendations(response.data.recommendations)
  }

  return (
    <div>
      <button onClick={getRecommendations}>Film Öner</button>
      <ul>
        {recommendations.map((rec, index) => <li key={index}>{rec}</li>)}
      </ul>
    </div>
  )
}
export default MoviesRecommender