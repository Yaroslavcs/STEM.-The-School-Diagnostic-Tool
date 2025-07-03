import React, { useEffect, useState } from 'react';
import './App.css';

// Тип для школи
interface School {
  id: number;
  name: string;
}

// Тип для всіх відповідей
interface SurveyData {
  fullName: string;
  class: string;
  schoolId: number | '';
  gender: string;
  // Математика
  math_boring_interesting?: number;
  math_unattractive_attractive?: number;
  math_everyday_amazing?: number;
  math_unexciting_exciting?: number;
  math_meaningless_meaningful?: number;
  // Наука
  science_everyday_amazing?: number;
  science_unattractive_attractive?: number;
  science_unexciting_exciting?: number;
  science_meaningless_meaningful?: number;
  science_boring_interesting?: number;
  // Інженерія
  eng_unattractive_attractive?: number;
  eng_everyday_amazing?: number;
  eng_meaningless_meaningful?: number;
  eng_unexciting_exciting?: number;
  eng_boring_interesting?: number;
  // Технології
  tech_unattractive_attractive?: number;
  tech_meaningless_meaningful?: number;
  tech_boring_interesting?: number;
  tech_unexciting_exciting?: number;
  tech_everyday_amazing?: number;
  // Кар'єра
  career_meaningless_meaningful?: number;
  career_boring_interesting?: number;
  career_unexciting_exciting?: number;
  career_everyday_amazing?: number;
  career_unattractive_attractive?: number;
}

const initialSurvey: SurveyData = {
  fullName: '',
  class: '',
  schoolId: '',
  gender: '',
};

const scale = [1, 2, 3, 4, 5, 6, 7];

type ScaleRowProps = {
  labelLeft: string;
  labelRight: string;
  name: keyof SurveyData;
  value?: number;
  onChange: (name: keyof SurveyData, value: number) => void;
};

const ScaleRow: React.FC<ScaleRowProps> = ({ labelLeft, labelRight, name, value, onChange }) => (
  <div className="scale-row">
    <span>{labelLeft}</span>
    {scale.map((n) => (
      <label key={n}>
        <input
          type="radio"
          name={name}
          value={n}
          checked={value === n}
          onChange={() => onChange(name, n)}
        />
        {n}
      </label>
    ))}
    <span>{labelRight}</span>
  </div>
);

const App: React.FC = () => {
  const [schools, setSchools] = useState<School[]>([]);
  const [survey, setSurvey] = useState<SurveyData>(initialSurvey);
  const [result, setResult] = useState<any>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    // Підтягуємо список шкіл
    fetch('/api/Schools/list')
      .then((r) => r.json())
      .then(setSchools)
      .catch(() => setSchools([]));
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setSurvey((prev) => ({ ...prev, [name]: value }));
  };

  const handleScale = (name: keyof SurveyData, value: number) => {
    setSurvey((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setResult(null);
    // Формуємо тіло запиту згідно з DTO
    const body = {
      fullName: survey.fullName,
      class: survey.class,
      schoolId: Number(survey.schoolId),
      gender: survey.gender,
      // Математика
      math_Boring_Interesting: survey.math_boring_interesting,
      math_Unattractive_Attractive: survey.math_unattractive_attractive,
      math_Everyday_Amazing: survey.math_everyday_amazing,
      math_Unexciting_Exciting: survey.math_unexciting_exciting,
      math_Meaningless_Meaningful: survey.math_meaningless_meaningful,
      // Наука
      science_Everyday_Amazing: survey.science_everyday_amazing,
      science_Unattractive_Attractive: survey.science_unattractive_attractive,
      science_Unexciting_Exciting: survey.science_unexciting_exciting,
      science_Meaningless_Meaningful: survey.science_meaningless_meaningful,
      science_Boring_Interesting: survey.science_boring_interesting,
      // Інженерія
      eng_Unattractive_Attractive: survey.eng_unattractive_attractive,
      eng_Everyday_Amazing: survey.eng_everyday_amazing,
      eng_Meaningless_Meaningful: survey.eng_meaningless_meaningful,
      eng_Unexciting_Exciting: survey.eng_unexciting_exciting,
      eng_Boring_Interesting: survey.eng_boring_interesting,
      // Технології
      tech_Unattractive_Attractive: survey.tech_unattractive_attractive,
      tech_Meaningless_Meaningful: survey.tech_meaningless_meaningful,
      tech_Boring_Interesting: survey.tech_boring_interesting,
      tech_Unexciting_Exciting: survey.tech_unexciting_exciting,
      tech_Everyday_Amazing: survey.tech_everyday_amazing,
      // Кар'єра
      career_Meaningless_Meaningful: survey.career_meaningless_meaningful,
      career_Boring_Interesting: survey.career_boring_interesting,
      career_Unexciting_Exciting: survey.career_unexciting_exciting,
      career_Everyday_Amazing: survey.career_everyday_amazing,
      career_Unattractive_Attractive: survey.career_unattractive_attractive,
    };
    try {
      const res = await fetch('/api/StudentGrades/survey-full', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(body),
      });
      const data = await res.json();
      setResult(data);
    } catch (e) {
      setResult({ error: 'Помилка відправки' });
    } finally {
      setLoading(false);
    }
  };

  // Підрахунок середнього для кожного блоку
  const getAverage = (keys: (keyof SurveyData)[]) => {
    const vals = keys.map((k) => survey[k]).filter((v) => typeof v === 'number') as number[];
    if (!vals.length) return '-';
    return (vals.reduce((a, b) => a + b, 0) / vals.length).toFixed(2);
  };

  return (
    <div className="App">
      <h1>STEM Semantics Survey</h1>
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>ПІБ*<input name="fullName" value={survey.fullName} onChange={handleChange} required /></label>
          <label>Клас*<input name="class" value={survey.class} onChange={handleChange} maxLength={32} required /></label>
          <label>Навчальний заклад*
            <select name="schoolId" value={survey.schoolId} onChange={handleChange} required>
              <option value="">Оберіть школу</option>
              {schools.map((s) => (
                <option key={s.id} value={s.id}>{s.name}</option>
              ))}
            </select>
          </label>
          <label>Стать
            <select name="gender" value={survey.gender} onChange={handleChange}>
              <option value="">Не вказано</option>
              <option value="чоловіча">Чоловіча</option>
              <option value="жіноча">Жіноча</option>
            </select>
          </label>
        </div>
        <h2>Для мене МАТЕМАТИКА — це...</h2>
        <ScaleRow labelLeft="нудна" labelRight="цікава" name="math_boring_interesting" value={survey.math_boring_interesting} onChange={handleScale} />
        <ScaleRow labelLeft="неприваблива" labelRight="приваблива" name="math_unattractive_attractive" value={survey.math_unattractive_attractive} onChange={handleScale} />
        <ScaleRow labelLeft="повсякденна" labelRight="дивовижна" name="math_everyday_amazing" value={survey.math_everyday_amazing} onChange={handleScale} />
        <ScaleRow labelLeft="незахоплюча" labelRight="захоплюча" name="math_unexciting_exciting" value={survey.math_unexciting_exciting} onChange={handleScale} />
        <ScaleRow labelLeft="нічого не означає" labelRight="багато значить" name="math_meaningless_meaningful" value={survey.math_meaningless_meaningful} onChange={handleScale} />
        <h2>Для мене НАУКА — це...</h2>
        <ScaleRow labelLeft="повсякденна" labelRight="дивовижна" name="science_everyday_amazing" value={survey.science_everyday_amazing} onChange={handleScale} />
        <ScaleRow labelLeft="неприваблива" labelRight="приваблива" name="science_unattractive_attractive" value={survey.science_unattractive_attractive} onChange={handleScale} />
        <ScaleRow labelLeft="незахоплюча" labelRight="захоплюча" name="science_unexciting_exciting" value={survey.science_unexciting_exciting} onChange={handleScale} />
        <ScaleRow labelLeft="нічого не означає" labelRight="багато значить" name="science_meaningless_meaningful" value={survey.science_meaningless_meaningful} onChange={handleScale} />
        <ScaleRow labelLeft="нудна" labelRight="цікава" name="science_boring_interesting" value={survey.science_boring_interesting} onChange={handleScale} />
        <h2>Для мене ІНЖЕНЕРІЯ — це...</h2>
        <ScaleRow labelLeft="неприваблива" labelRight="приваблива" name="eng_unattractive_attractive" value={survey.eng_unattractive_attractive} onChange={handleScale} />
        <ScaleRow labelLeft="повсякденна" labelRight="дивовижна" name="eng_everyday_amazing" value={survey.eng_everyday_amazing} onChange={handleScale} />
        <ScaleRow labelLeft="нічого не означає" labelRight="багато значить" name="eng_meaningless_meaningful" value={survey.eng_meaningless_meaningful} onChange={handleScale} />
        <ScaleRow labelLeft="незахоплюча" labelRight="захоплюча" name="eng_unexciting_exciting" value={survey.eng_unexciting_exciting} onChange={handleScale} />
        <ScaleRow labelLeft="нудна" labelRight="цікава" name="eng_boring_interesting" value={survey.eng_boring_interesting} onChange={handleScale} />
        <h2>Для мене ТЕХНОЛОГІЇ — це...</h2>
        <ScaleRow labelLeft="неприваблива" labelRight="приваблива" name="tech_unattractive_attractive" value={survey.tech_unattractive_attractive} onChange={handleScale} />
        <ScaleRow labelLeft="нічого не означає" labelRight="багато значить" name="tech_meaningless_meaningful" value={survey.tech_meaningless_meaningful} onChange={handleScale} />
        <ScaleRow labelLeft="нудна" labelRight="цікава" name="tech_boring_interesting" value={survey.tech_boring_interesting} onChange={handleScale} />
        <ScaleRow labelLeft="незахоплюча" labelRight="захоплюча" name="tech_unexciting_exciting" value={survey.tech_unexciting_exciting} onChange={handleScale} />
        <ScaleRow labelLeft="повсякденна" labelRight="дивовижна" name="tech_everyday_amazing" value={survey.tech_everyday_amazing} onChange={handleScale} />
        <h2>Для мене КАР'ЄРА в науці, математиці, інженерії чи математиці — це...</h2>
        <ScaleRow labelLeft="нічого не означає" labelRight="багато значить" name="career_meaningless_meaningful" value={survey.career_meaningless_meaningful} onChange={handleScale} />
        <ScaleRow labelLeft="нудна" labelRight="цікава" name="career_boring_interesting" value={survey.career_boring_interesting} onChange={handleScale} />
        <ScaleRow labelLeft="незахоплюча" labelRight="захоплюча" name="career_unexciting_exciting" value={survey.career_unexciting_exciting} onChange={handleScale} />
        <ScaleRow labelLeft="повсякденна" labelRight="дивовижна" name="career_everyday_amazing" value={survey.career_everyday_amazing} onChange={handleScale} />
        <ScaleRow labelLeft="неприваблива" labelRight="приваблива" name="career_unattractive_attractive" value={survey.career_unattractive_attractive} onChange={handleScale} />
        <button type="submit" disabled={loading}>{loading ? 'Відправка...' : 'Надіслати'}</button>
      </form>
      {result && (
        <div className="result">
          <h2>Результат</h2>
          <pre>{JSON.stringify(result, null, 2)}</pre>
          <h3>Середні оцінки:</h3>
          <ul>
            <li>Математика: {getAverage(['math_boring_interesting','math_unattractive_attractive','math_everyday_amazing','math_unexciting_exciting','math_meaningless_meaningful'])}</li>
            <li>Наука: {getAverage(['science_everyday_amazing','science_unattractive_attractive','science_unexciting_exciting','science_meaningless_meaningful','science_boring_interesting'])}</li>
            <li>Інженерія: {getAverage(['eng_unattractive_attractive','eng_everyday_amazing','eng_meaningless_meaningful','eng_unexciting_exciting','eng_boring_interesting'])}</li>
            <li>Технології: {getAverage(['tech_unattractive_attractive','tech_meaningless_meaningful','tech_boring_interesting','tech_unexciting_exciting','tech_everyday_amazing'])}</li>
            <li>Кар'єра: {getAverage(['career_meaningless_meaningful','career_boring_interesting','career_unexciting_exciting','career_everyday_amazing','career_unattractive_attractive'])}</li>
          </ul>
        </div>
      )}
    </div>
  );
};

export default App;
