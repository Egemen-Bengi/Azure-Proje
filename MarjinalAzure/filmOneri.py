import os  
from openai import AzureOpenAI  

endpoint = os.getenv("ENDPOINT_URL", "https://egeme-m9mpbugp-eastus2.openai.azure.com/openai/deployments/gpt-4o/chat/completions?api-version=2025-01-01-preview")  
deployment = os.getenv("DEPLOYMENT_NAME", "gpt-4o")  
subscription_key = os.getenv("AZURE_OPENAI_API_KEY", "7uJKaNCyx668ljGM5QTzTgM41XLk3xRdZJn5vSMaTd8eYPduKPQ5JQQJ99BDACHYHv6XJ3w3AAAAACOGBLpR")  
    
client = AzureOpenAI(  
    azure_endpoint=endpoint,  
    api_key=subscription_key,  
    api_version="2025-01-01-preview",
)
    
    
chat_prompt = [
    {
        "role": "system",
        "content": [
            {
                "type": "text",
                "text": "Kullanıcıların aradığı bilgiyi bulmasına yardımcı olan bir yapay zeka yardımcısısınız."
            }
        ]
    }
] 
    
def filmOneriBot(userPreferences) :
    prompt = f"kullanıcının tercihleri: {', '.join(userPreferences)}. Bu kullanıcıya 3 tane film öner."
    
    chat_prompt = [
        {
            "role": "system",
            "content": "Sen bir film önerme asisstantısın. Kullanıcının tercihleri doğrultusunda ona film öner."
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

print(filmOneriBot(["inception", "interstellar", "the dark knight"]))