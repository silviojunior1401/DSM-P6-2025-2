import joblib

loaded_model = joblib.load('heart_modelo_lda.joblib')

import numpy as np
import pandas as pd

novos = [[40,140,289,172,0,1,2,0,0,0,1],[49,160,180,156,10,0,3,0,0,0,2],[37,130,283,98,0,1,2,0,1,0,1],[48,138,214,108,15,0,4,0,0,1,2],[54,150,195,122,0,1,3,0,0,0,1]]

# Criar um DataFrame com os novos dados
novos_df = pd.DataFrame(novos, columns=['age', 'resting bp s', 'cholesterol', 'max heart rate', 'oldpeak', 'sex', 'chest pain type', 'fasting blood sugar', 'resting ecg', 'exercise angina', 'ST slope'])

# Define as colunas para normalização
cols_to_normalize = ['age', 'resting bp s', 'cholesterol', 'max heart rate', 'oldpeak']

# Separa as colunas a serem normalizadas e as demais
novos_norm_subset = novos_df[cols_to_normalize].values
novos_other_subset = novos_df.drop(columns=cols_to_normalize).values

# Normaliza as colunas especificadas usando a média e o desvio padrão do conjunto de treinamento
# Valores de média e desvio padrão do conjunto de treinamento
mu = [53.51089325,132.39651416,198.79956427,136.80936819,0.88736383]
sigma = [9.43261651,18.51415412,109.38414455,25.46033414,1.06657015]

# Aplicar a normalização
novos_scaled_subset = (novos_norm_subset - mu) / sigma

# Combinar os dados normalizados com as colunas não normalizadas
novos_scaled = np.hstack((novos_scaled_subset, novos_other_subset))

previsao = loaded_model.predict(novos_scaled)

print(previsao)