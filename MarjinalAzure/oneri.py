import os  
from openai import AzureOpenAI
from dotenv import load_dotenv

load_dotenv()

api_key = os.getenv("AZURE_OPENAI_API_KEY")
endpoint = os.getenv("ENDPOINT_URL")

endpoint = os.getenv("ENDPOINT_URL", endpoint)  
deployment = os.getenv("DEPLOYMENT_NAME", "gpt-4o")  
subscription_key = os.getenv("AZURE_OPENAI_API_KEY", api_key)  

client = AzureOpenAI(  
    azure_endpoint=endpoint,  
    api_key=subscription_key,  
    api_version="2025-01-01-preview",
)

def eddOneriBot(userPreferences) :
    prompt = f"kullanıcının tercihleri: {', '.join(userPreferences)}. Bu kullanıcıya 3 tane dizi öner."
    
    chat_prompt = [
        {
            "role": "system",
            "content": "Sen bir dizi önerme asisstantısın. Kullanıcının tercihleri doğrultusunda ona dizi öner."
        },
        {
            "role": "user",
            "content": prompt
        }
    ]
    messages = chat_prompt

    completion = client.chat.completions.create(  
        model=deployment,
        messages=messages,
        max_tokens=800,  
        temperature=0.7,  
        top_p=0.95,  
        frequency_penalty=0,  
        presence_penalty=0,
        stop=None,  
        stream=False
    )

    return completion.choices[0].message.content
