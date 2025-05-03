import sys
import os
import pickle
from langchain_ollama.llms import OllamaLLM
from langchain_core.prompts import ChatPromptTemplate
from langchain.memory import ConversationBufferMemory
from langchain.chains import LLMChain
from langchain_ollama import OllamaEmbeddings
from langchain_chroma import Chroma
from langchain_core.documents import Document
import pandas as pd
import UnityEngine as ue


# Game Object
object = ue.GameObject.Find("CIM")
object = object.GetComponent("CommandInputManager")

# Vector
df = pd.read_csv(os.getcwd() + "/Assets/Scripts/Matador/Ghosts.csv")
embeddings = OllamaEmbeddings(model="mxbai-embed-large")

db_location = "./chrome_langchain_db"
add_documents = not os.path.exists(db_location)

if add_documents:
    documents = []
    ids = []
    
    for i, row in df.iterrows():
        document = Document(
            page_content=row["Ghosts"] + " " + row["Their_manner_of_speaking"] + " " + row["What_each_ghost_knows_about_the_Thief"]+ " " + row["Each_ghosts_past_life"]+ " " + row["Each_ghosts_opinion_about_the_other_ghosts"],
            metadata={"Ghosts": row["Ghosts"], "Their_manner_of_speaking": row["Their_manner_of_speaking"], "What_each_ghost_knows_about_the_Thief": row["What_each_ghost_knows_about_the_Thief"], "Each_ghosts_past_life": row["Each_ghosts_past_life"],  "Each_ghosts_opinion_about_the_other_ghosts": row["Each_ghosts_opinion_about_the_other_ghosts"]},
            id=str(i)
        )
        ids.append(str(i))
        documents.append(document)
        
vector_store = Chroma(
    collection_name="ghostthoughts",
    persist_directory=db_location,
    embedding_function=embeddings
)

if add_documents:
    vector_store.add_documents(documents=documents, ids=ids)
    
retriever = vector_store.as_retriever(
    search_kwargs={"k": 5}
)




# Set up model and prompt
model = OllamaLLM(model="llama3.2", max_tokens=12)

template = """
You are Victoria the ghost. You must provide dialogue that reflects your proclivities as a rich snob.
Be slightly hesistant to talk about what you know about the suspect until they get to know you better.

Here is the conversation so far:
{history}

Here are some relevant info: {reply}

Here is the dialogue to answer: {dialogue}
"""
prompt = ChatPromptTemplate.from_template(template)

# File to store memory
memory_file = "victoriamemory.pkl"

# Load existing memory or create new one
if os.path.exists(memory_file):
    with open(memory_file, "rb") as f:
        memory = pickle.load(f)
else:
    memory = ConversationBufferMemory(memory_key="history", input_key="dialogue")

# Set up the chain
chain = LLMChain(llm=model, prompt=prompt, memory=memory)

# Get dialogue from command line
dialogue = object.inputField.text #sys.argv[1] if len(sys.argv) > 1 else "say dot"

# Retrieve context and generate reply
reply = retriever.invoke(dialogue)
result = chain.invoke({"reply": reply, "dialogue": dialogue})

# Print result for Unity
print(result["text"])
object.texthere.SetText(result["text"])

# Save updated memory back to file
with open(memory_file, "wb") as f:
    pickle.dump(memory, f)
